/// <binding ProjectOpened='sass-watch' />

'use strict';

// NEED TO SET BINDINGS IN Task Runner Explorer - right-click on gulpfile.js
var gulp = require('gulp');
var sass = require('gulp-sass');
var sourcemaps = require('gulp-sourcemaps');
var autoprefixer = require('gulp-autoprefixer');
var minifyjs = require('gulp-uglify');
var concat = require('gulp-concat');

var autoprefixerOptions = {
    browsers: ['last 2 versions', '> 5%', 'Firefox ESR']
};

gulp.task('minify-js', function () {
    gulp.src(['./content/js/xxx.js', './content/js/yyy.js'])
      .pipe(concat('website.min.js'))
      .pipe(minifyjs())
      .pipe(gulp.dest('./content/js'));
});

gulp.task('sass-compile', function () {
    gulp.src('./content/css/scss/styles.scss')
        .pipe(sourcemaps.init())
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(sourcemaps.write('maps'))
        .pipe(autoprefixer(autoprefixerOptions))
        .pipe(gulp.dest('./content/css'));
});

// set bindings in Task Runner Explorer to 'project open'
gulp.task('sass-watch', function () {
    gulp.watch('./content/css/scss/*.scss', ['sass-compile']);
    gulp.watch('./content/css/scss/**/*.scss', ['sass-compile']);
});