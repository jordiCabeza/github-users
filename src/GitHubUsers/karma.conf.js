/* global module */
module.exports = function (config) {
    'use strict';
    config.set({
        autoWatch: true,
        singleRun: true,
        frameworks: ['jspm', 'jasmine'],
        files: [
          'jspm_packages/github/github/fetch*/fetch.js',
          'jspm_packages/github/components/jquery*/jquery.js',
          'jspm_packages/github/knockout/knockout*/dist/knockout.debug.js',
          'jspm_packages/github/Knockout-Contrib/Knockout-Validation*/dist/knockout.validation.js'
        ],
        jspm: {
            loadFiles: [
              'js/**/*.spec.js'
            ],
            serveFiles: [
              'js/**/*.html',
              'js/**/!(*spec).js',
              'js/!(build).js'
            ]
        },
        proxies: {
            '/base/js/': 'js/',
            '/base/jspm_packages/': 'jspm_packages/'
        },
        browsers: ['PhantomJS'],
        preprocessors: {
            'js/**/*spec.js': ['babel'],
            'js/jspm_packages/abcam/**/*.js': ['babel'],
            'js/**/!(*spec).js': ['babel']
        },
        babelPreprocessor: {
            options: {
                sourceMap: 'inline',
                blacklist: ['useStrict']
            },
            sourceFileName: function (file) {
                return file.originalPath;
            }
        },
        reporters: ['spec']
    });
};