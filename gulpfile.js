var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
var debug = require("gulp-debug");
var request = require('request');
var fs = require('fs');
var cheerio = require('cheerio');
var semver = require('semver');
var color = require('gulp-color');
var nuget = require('gulp-nuget');
var rimraf = require('gulp-rimraf');
var runSequence = require("run-sequence");

var outputDir = "./nupackages";
var pathToNugetExe = "nuget.exe";
var nugetFeed = "https://www.nuget.org/api/v2/package";
var nugetApiKey;

var nuspecFiles = [
	"./nuspec/xWrap.Framework.nuspec",
	"./nuspec/xWrap.nuspec",
	"./nuspec/xWrap.Mvc.Framework.nuspec",
	"./nuspec/xWrap.Mvc.nuspec"
];

gulp.task("pack", function (done) {
	loadConfiguration();
	return runSequence('_download-nuget', '_build-solution', '_pack-nuspecs', done);
});

gulp.task("increment-and-pack", function (done) {
	loadConfiguration();
	return runSequence('_download-nuget', '_build-solution', '_increment-nuspec-versions', '_pack-nuspecs', done);
});

gulp.task("push", function (done) {
	loadConfiguration();

	console.log(
		color("Pushing packages to nuget feed ", "WHITE") +
		color(nugetFeed, "YELLOW") +
		color(" with API key ", "WHITE") +
		color(nugetApiKey, "MAGENTA"));

	return gulp.src(outputDir + "/*.@(nupkg)")
		.pipe(nuget.push({
			source: nugetFeed,
			nuget: pathToNugetExe,
			apiKey: nugetApiKey
		}));
});


gulp.task('_download-nuget', function (done) {
	if (fs.existsSync(pathToNugetExe)) {
		return done();
	}

	request.get('https://dist.nuget.org/win-x86-commandline/latest/nuget.exe')
		.pipe(fs.createWriteStream(pathToNugetExe))
		.on('close', done);
});

gulp.task("_build-solution", function () {
	return gulp.src("./src/xwrap.sln")
		.pipe(debug({ title: "Building project:" }))
		.pipe(msbuild({
			targets: ['Clean', 'Rebuild'],
			stdout: true,
			configuration: "Debug",
			logCommand: false,
			verbosity: "minimal",
			maxcpucount: 0,
			toolsVersion: 'auto'
			})
		);
});

gulp.task("_increment-nuspec-versions", function () {
	return nuspecFiles.forEach(function (nuspec) {
		var version = getNugetVersion(nuspec);
		version = semver.inc(version, "patch");
		saveNugetVersion(nuspec, version);
	});
});

gulp.task("_pack-nuspecs", function () {
	return nuspecFiles.forEach(function (nuspec) {
		emptyDirectory(outputDir + "/*");
		packNugetPackage(nuspec, outputDir);
	});
});

function emptyDirectory(directory) {
	return gulp.src(directory, { read: false }).pipe(rimraf({ force: true }));
}

function packNugetPackage(nuspecPath, outputDir) {
	var version = getNugetVersion(nuspecPath);

	return gulp.src(nuspecPath)
		.pipe(nuget.pack({
			nuget: pathToNugetExe,
			version: version,
			outputDirectory: outputDir
		}));
}

function getNugetVersion(nuspecPath) {
	var $ = cheerio.load(fs.readFileSync(nuspecPath), { xmlMode: true });
	return $('version').text();
}

function saveNugetVersion(nuspecPath, version) {
	console.log(
		color("New version for '", "WHITE") +
		color(nuspecPath, "YELLOW") +
		color("' package is being incremented to ", "WHITE") +
		color(version, "MAGENTA"));

	var $ = cheerio.load(fs.readFileSync(nuspecPath), { xmlMode: true });

	$('version').text(version);
	$('dependency').each(function() {
		$(this).attr("version", version);
	});

	return fs.writeFileSync(nuspecPath, $.xml());
}

function loadConfiguration() {
	var configFileName = './gulp.config.json';
	var config = require(configFileName);
	nugetApiKey = config.nugetApiKey;
}