var gulp = require("gulp"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    sass = require("gulp-sass"),
    rimraf = require("rimraf"),
    fs = require("fs"),
    less = require("gulp-less");


var webrootFolder = "wwwroot";
var paths = {
    webroot: "./" + webrootFolder + "/"
};

paths.css = paths.webroot + "css/**/*.css";

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.concatJsDest = paths.webroot + "js/site.min.js";

gulp.task("less", function () {
    return gulp.src(’Styles/main.less’)
.pipe(less())
        .pipe(gulp.dest(’wwwroot/css’));
});

gulp.task("sass", function () {
    return gulp.src(’Styles/main2.scss’)
.pipe(sass())
        .pipe(gulp.dest(’wwwroot/css’));
});


gulp.task("bundleJS", function () {
    gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("bundleCSS", function () {
    gulp.src(paths.css)
        .pipe(cssmin())
        .pipe(gulp.dest(’dist’));
});

gulp.task("bundleAll", function() {
    runSequence(’bundleJS’, ’bundleCS’);
});
