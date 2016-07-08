/// <binding BeforeBuild='default' />
'use strict';

var gulp = require('gulp');
var jscs = require('gulp-jscs');
var jshint = require('gulp-jshint');
var Server = require('karma').Server;

var sources = [
  'wwwroot/js/*.js',
  'wwwroot/js/**/*.js',
  '*.js'
];

gulp.task('lint', function() {
  return gulp.src(sources)
    .pipe(jshint())
    .pipe(jshint.reporter('default'))
    .pipe(jscs())
    .pipe(jscs.reporter())
    .pipe(jshint.reporter('fail'))
    .pipe(jscs.reporter('fail'));
});

gulp.task('default', ['lint'], function(done) {
  new Server({
    configFile: __dirname + '/test/config/karma.conf.js',
    singleRun: true
  }, done).start();
});

gulp.task('watch', function() {
  gulp.watch([sources], ['default']);
});
