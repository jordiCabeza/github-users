// Node modules
var Server = require('karma').Server, exec = require('child_process').exec;

// Gulp and plugins
var gulp = require('gulp');   


gulp.task('karma', function (done) {
    var karmaServer = new Server({
        configFile: __dirname + '/karma.conf.js',
        singleRun: true
    }, done);

    karmaServer.start();
});

gulp.task('bundle-jspm', function (cb) {
    return exec('jspm bundle js/app js/build.js --minify --inject', function (err, stdout, stderr) {
        console.log(stdout);
        console.log(stderr);
        cb(err);
    });
});
