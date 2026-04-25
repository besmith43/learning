const { defineConfig } = require('@playwright/test');
const path = require('path');

module.exports = defineConfig({
  testDir: __dirname,
  timeout: 30_000,
  workers: 1,
  webServer: {
    command: 'mvn -q -Dmaven.repo.local=.m2/repository -DskipTests compile org.codehaus.mojo:exec-maven-plugin:3.6.2:java -Dexec.mainClass=com.example.opendst.orders.http.LocalClusterMain -Dexec.args=18080',
    cwd: path.resolve(__dirname, '../..'),
    url: 'http://127.0.0.1:18080/health',
    reuseExistingServer: false,
    timeout: 60_000
  },
  use: {
    baseURL: 'http://127.0.0.1:18080'
  }
});
