const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:1452';

const context =  [
    "/weatherforecast",
    "/test",
    "/product",
    "/detailedproduct",
    "/person",
    "/address",
    "/cart",
    "/category",
    "/payment",
    "/producttocart",
    "/producttocategory",
    "/sale"
];

module.exports = function(app) {
  const appProxy = createProxyMiddleware(context, {
    proxyTimeout: 10000,
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};
