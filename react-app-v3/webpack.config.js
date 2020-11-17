const path = require('path');
const webpack = require('webpack');
//const config = require('config');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

//var commonsPlugin = new webpack.optimize.CommonsChunkPlugin('common.js');
//const WebpackMd5Hash = require('webpack-md5-hash');

/*
const htmlPlugin = new HtmlWebPackPlugin({
  template: "./public/index.html"
});
*/

module.exports = {
  
  mode: 'development',

  //VERSION 1: single-entry
  entry: ["@babel/polyfill", "./src/index.js"],
  
  output: {
    //VERSION 1: single-bundle
      path: path.resolve(__dirname, 'public'),
      filename: 'bundle.js'

    //VERSION 2: dual-bundle
    //path: path.resolve(__dirname, 'public'),
    //filename: "[name].bundle.js"
  },
  devtool: 'inline-source-map',
  /*
  devServer: {
      contentBase: './public',
      //contentBase: path.join(__dirname, 'public'),
      historyApiFallback: true
      //hot: true, 
      //open: true
  },
  */
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: [{ loader: 'babel-loader' }]
      },
      { 
        test: /\.s?css$/,
        use: ['style-loader', 'css-loader', 'sass-loader']
      },
      {
        test: /\.html$/,
        loader: 'html-loader'
      }
    ]
  },
  plugins: [
      /*
      new CleanWebpackPlugin({
          cleanAfterEveryBuildPatterns: ['dist']
      }),
      */
      new CleanWebpackPlugin(),
      new HtmlWebpackPlugin({
          //inject: false,
          //hash: true,
          template: './public/index.html'
          //filename: 'index.html'
      
      })
    ]
      //}),
      //new WebpackMd5Hash()
   
}

