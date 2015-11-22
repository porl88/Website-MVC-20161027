/// <binding ProjectOpened='sass-watch' />
// NEED TO SET BINDINGS IN Task Runner Explorer - right-click on gulpfile.js
var gulp = require('gulp');
var sass = require('gulp-sass');

gulp.task('sass-compile', function () {
    gulp.src('./content/css/scss/styles.scss')
        .pipe(sass({ outputStyle: 'expanded' }))
        .pipe(gulp.dest('./content/css/'));
});

// set bindings in Task Runner Explorer to 'project open'
gulp.task('sass-watch', function () {
    gulp.watch('./content/css/sass/**/*.scss', ['sass-compile']);
});