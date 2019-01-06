var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
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

gulp.task("_build-solution", function () {
	return gulp.src("./src/xwrap.sln")
		.pipe(msbuild({
				targets: ['Clean', 'Build']
			})
		);
});

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