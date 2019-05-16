var path = require('path');

module.exports = env => {
    env = env || {};

    var output = {
        publicPath: '/dist/',
        filename: '[name].js'
    };

    // Only create files in the target directory if it's a production environment.
    if (env.production) {
        output.path = path.join(__dirname, 'wwwroot', 'dist');
    }

    return {
        entry: { 'index': './Client/index.ts' },
        resolve: {
            extensions: ['.js', '.ts']
        },
        module: {
            rules: [
                { test: /\.ts$/, use: 'ts-loader' }
            ]
        },
        output: output
    };
}