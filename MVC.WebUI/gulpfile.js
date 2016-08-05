/// <binding ProjectOpened='sass-watch' />

'use strict';

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
    gulp.src(['./content/js/xxx.js', './content/js/yyy.js'])
      .pipe(concat('website.min.js'))
      .pipe(minifyjs())
      .pipe(gulp.dest('./content/js'));
});

gulp.task('sass-compile', function () {
    gulp.src('./content/website/css/scss/website.scss')
        .pipe(sourcemaps.init())
        .pipe(autoprefixer(autoprefixerOptions)) // does not work with SASS??? - possibly issue with sourcemaps - might need to call sourcemaps twice: https://github.com/sindresorhus/gulp-autoprefixer/issues/8, https://github.com/floridoo/gulp-sourcemaps/issues/60
        .pipe(sass({ outputStyle: 'compressed' }))
        .pipe(sourcemaps.write('maps'))
        .pipe(gulp.dest('./content/css'));
});

gulp.task('default', function () {
    return gulp.src('src/**/*.css')
        .pipe(sourcemaps.init())
        .pipe(autoprefixer())
        .pipe(concat('all.css'))
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('dist'));
});

// set bindings in Task Runner Explorer to 'project open'
gulp.task('sass-watch', function () {
    gulp.watch('./content/css/scss/*.scss', ['sass-compile']);
    gulp.watch('./content/css/scss/**/*.scss', ['sass-compile']);
});









/* POST CSS */
/*
var postcss = require('gulp-postcss'); // https://www.npmjs.com/package/gulp-postcss
var autoprefixer = require('autoprefixer'); // https://www.npmjs.com/package/autoprefixer, Autoprefixer CSS online: http://autoprefixer.github.io/
var cssnano = require('cssnano'); // https://www.npmjs.com/package/cssnano

gulp.task('postcss', function () {
    var processors = [
        autoprefixer({ browsers: ['Firefox ESR', 'Firefox >= 38', 'IE >= 8', 'Edge >= 12', 'last 4 Chrome versions', 'last 4 Opera versions', 'Safari >= 7', 'IOS >= 7'] }), // browserlist config: https://github.com/ai/browserslist#queries
        cssnano(),
    ];
    return gulp.src('./content/website/css/website.css')
        .pipe(postcss(processors))
        .pipe(gulp.dest('./dest'));
});
*/