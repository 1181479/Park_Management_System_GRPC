k6 run grpcAllParks.js
timeout /t 60
k6 run grpcPartialParks.js
timeout /t 60
k6 run grpcCreateUser.js
timeout /t 60
k6 run grpcAllParksClientStream.js
timeout /t 60
k6 run grpcAllParksServerStream.js
timeout /t 60
k6 run grpcAllParksTwoSidedStream.js
timeout /t 60
k6 run grpcUpdateParkingValue.js
timeout /t 60
k6 run grpcLeavePark.js