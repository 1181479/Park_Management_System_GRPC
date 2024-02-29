k6 run grpcAllParks.js --out web-dashboard=export=grpcAllParks-test-report.html
k6 run grpcPartialParks.js --out web-dashboard=export=grpcPartialParks-test-report.html
k6 run grpcCreateUser.js --out web-dashboard=export=grpcCreateUser-test-report.html