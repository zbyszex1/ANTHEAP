const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
  "/weatherforecast",
  "/accountnumber",
  "/authorizedclerk",
  "/partner",
  "/representative",
  "/query",
  "/taxpayer",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7170',
        secure: false
    });

    app.use(appProxy);
};
