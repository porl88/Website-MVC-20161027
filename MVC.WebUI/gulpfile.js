/// <binding ProjectOpened='sass-watch' />

'use strict';
// N.B. Might need to install the latest version of Node.js from https://nodejs.org/en/ - and then configure Visual Studio to use it http://josharepoint.com/2016/05/04/how-to-configure-visual-studio-2015-integration-with-latest-version-of-node-js-and-npm/
// NEED TO SET BINDINGS IN Task Runner Explorer - right-click on gulpfile.js
var gulp = require('gulp');
var sass = require('gulp-sass');
var sourcemaps = require('gulp-sourcemaps');
var autoprefixer = require('gulp-autoprefixer'); // https://www.npmjs.com/package/gulp-autoprefixer - does not work with SASS??? need to use Post CSS instead???
var minifyjs = require('gulp-uglify');
var concat = require('gulp-concat');

// browserlist config: https://github.com/ai/browserslist#queries
var autoprefixerOptions = {
    browsers: ['Firefox ESR', 'Firefox >= 38', 'IE >= 8', 'Edge >= 12', 'last 4 Chrome versions', 'last 4 Opera versions', 'Safari >= 7', 'IOS >= 7']
};

gulp.task('minify-js', function () {
    gulp.src(['./content/website/js/_polyfill/*.js', './content/website/js/_core/*.js'])
      .pipe(sourcemaps.init())
      .pipe(concat('website.min.js'))
      .pipe(minifyjs())
      .pipe(sourcemaps.write('_maps'))
      .pipe(gulp.dest('./content/js'));
});

gulp.task('sass-compile', function () {
    gulp.src('./content/website/css/scss/website.scss')
        .pipe(sourcemaps.init())
        .pipe(autoprefixer(autoprefixerOptions))
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(sourcemaps.write('_maps'))
        .pipe(gulp.dest('./content/css'));
});

// set bindings in Task Runner Explorer to 'project open'
gulp.task('sass-watch', function () {
    gulp.watch('./content/css/scss/*.scss', ['sass-compile']);
    gulp.watch('./content/css/scss/**/*.scss', ['sass-compile']);
});


