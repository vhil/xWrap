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

function loadConfiguration() {
	var configFileName = './gulp.config.json';
	var config = require(configFileName);
	nugetApiKey = config.nugetApiKey;
}

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

//var runSequence = require("run-sequence");
//var merge = require('merge-stream');
//var rimraf = require('gulp-rimraf');
//var path = require('path');
//var color = require('gulp-color');

//var config;
//var dest;
//var configuration;
//var projects;
//var filesToRemoveBefore;
//var filesToRemoveAfter;
//var deployConent;
//var deploySerializedItems;

//gulp.task("_clean-before", function () {
//	var _merge = new merge();
//	filesToRemoveBefore.forEach(function (fileSrc) {
//		var toClean = path.join(dest, fileSrc);
//		console.log("Clean Before: " + toClean);
//		_merge.add(gulp.src(toClean, { read: false }).pipe(rimraf({ force: true })));
//	});
//	return _merge.isEmpty() ? null : _merge;
//});

//gulp.task("_clean-after", function () {
//	var _merge = new merge();
//	filesToRemoveAfter.forEach(function (fileSrc) {
//		var toClean = path.join(dest, fileSrc);
//		console.log("Clean After: " + toClean);
//		_merge.add(gulp.src(toClean, { read: false }).pipe(rimraf({ force: true })));
//	});
//	return _merge.isEmpty() ? null : _merge;
//});

//gulp.task("_convert-entities", function () {
//	var _merge = new merge();
//	console.log("Converting Entities: " + dest);
//	_merge.add(gulp.src(dest + "/**/*.@(html|cshtml|json|xslt|ascx|aspx)").pipe(entityconvert()));
//	return _merge.isEmpty() ? null : _merge;
//});

//gulp.task("_zip", function (cb) {
//	var _merge = new merge();
//	var zerofill = function (i) {
//		return (i < 10 ? '0' : '') + i;
//	}
//	var date = new Date();
//	var today = date.getFullYear().toString() + zerofill(date.getMonth() + 1) + zerofill(date.getDate());
//	var zipFileName = "Veks." + configuration + '.' + today + '.zip';
//	console.log(color("Zipping to: ", "CYAN") + color(path.resolve(dest, '..', zipFileName), "YELLOW"));

//	_merge.add(gulp.src(dest + '/**/*').pipe(zip(zipFileName)).pipe(gulp.dest(path.resolve(dest, '..'))));
//	_merge.isEmpty() ? null : _merge;
//});

//gulp.task("build:Debug", function (cb) {
//	loadConfiguration('Debug');
//	return runSequence('_clean-before', '_publish-website', cb);
//});

//gulp.task("build:Staging", function (cb) {
//	loadConfiguration('Staging');
//	return runSequence('_clean-before', ['_publish-website', '_copy-sitecore-items'], '_clean-after', '_convert-entities', '_zip', cb);
//});

//gulp.task("build:Release", function (cb) {
//	loadConfiguration('Release');
//	return runSequence('_clean-before', ['_publish-website', '_copy-sitecore-items'], '_clean-after', '_convert-entities', '_zip', cb);
//});

//function loadConfiguration(configurationName) {
//	var configFileName = './gulpfile.config.' + configurationName + '.json';

//	try {
//		config = require('./gulpfile.config.' + configurationName + '.json');
//		console.log('Using ' + color(path.resolve(configFileName), "YELLOW"));
//		dest = path.resolve(config.websiteRoot);
//		configuration = config.buildConfiguration;
//		projects = config.projects;
//		filesToRemoveBefore = config.filesToRemoveBefore;
//		filesToRemoveAfter = config.filesToRemoveAfter;
//		deployConent = config.deployContent;
//		deploySerializedItems = config.deploySerializedItems;
//	} catch (e) {
//		if (e.code === 'MODULE_NOT_FOUND') {
//			console.log(path.resolve(configFileName));
//			process.exit(1);
//		}
//	}
//}

//gulp.task("_copy-sitecore-items", function (cb) {
//	var _merge = new merge();
//	var projectFiles = [];
//	var ignores = ['./**/node_modules'];

//	if (!deploySerializedItems) {
//		console.log(color('Serialization items copying is not enabled for this configuration.', "RED"));
//		return _merge.isEmpty() ? null : _merge;
//	}

//	projects.forEach(function (project) {
//		if (project.startsWith('!')) {
//			ignores.push(project.substring(1));
//		}
//	});
//	projects.forEach(function (project) {
//		var files = glob.sync(project, { root: './src', ignore: ignores });
//		files.forEach(function (file) {
//			projectFiles.push(file);
//		});
//	});

//	projectFiles.forEach(function (projectFile) {
//		var dir = path.dirname(projectFile);
//		var dirName = path.basename(dir);

//		var source = dir + '/serialization/**/*';
//		var target = dest + '/App_Data/' + '/serialization/' + dirName + '/serialization';

//		console.log(
//			color("Copying serialization items from ", "YELLOW") +
//			color("'" + source + "'", "CYAN") +
//			color(" to ", "YELLOW") +
//			color("'" + target + "'", "CYAN"));

//		_merge.add(gulp.src(source).pipe(gulp.dest(target)));

//		if (deployConent) {
//			var contentSource = dir + '/serialization.content/**/*';
//			var contentTarget = dest + '/App_Data/' + '/serialization/' + dirName + '/serialization.content';

//			console.log(
//				color("Copying CONTENT serialization items from '", "MAGENTA") +
//				color("'" + contentSource + "'", "CYAN") +
//				color(" to ", "MAGENTA") +
//				color("'" + contentTarget + "'", "CYAN"));

//			_merge.add(gulp.src(contentSource).pipe(gulp.dest(contentTarget)));
//		}
//	});
//	return _merge.isEmpty() ? null : _merge;
//});