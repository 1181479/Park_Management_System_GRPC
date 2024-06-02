@REM k6 run --out csv=grpcAllParks.csv grpcAllParks.js
@REM timeout /t 60
@REM k6 run --out csv=grpcPartialParks.csv grpcPartialParks.js
@REM timeout /t 60
@REM k6 run --out csv=grpcCreateUser.csv grpcCreateUser.js
@REM timeout /t 60
@REM k6 run --out csv=grpcAllParksClientStream.csv grpcAllParksClientStream.js
@REM timeout /t 60
@REM k6 run --out csv=grpcAllParksServerStream.csv grpcAllParksServerStream.js
@REM timeout /t 60
@REM k6 run --out csv=grpcAllParksTwoSidedStream.csv grpcAllParksTwoSidedStream.js
@REM timeout /t 60
@REM k6 run --out csv=grpcUpdateParkingValue.csv grpcUpdateParkingValue.js
@REM timeout /t 60
@REM k6 run --out csv=grpcLeavePark.csv grpcLeavePark.js
@REM timeout /t 60
@REM k6 run --out csv=grpcLeaveParkClientStream.csv grpcLeaveParkClientStream.js
@REM timeout /t 60
@REM k6 run --out csv=grpcLeaveParkServerStream.csv grpcLeaveParkServerStream.js
@REM timeout /t 60
k6 run --out csv=grpcLeaveParkTwoSideStream.csv grpcLeaveParkTwoSideStream.js