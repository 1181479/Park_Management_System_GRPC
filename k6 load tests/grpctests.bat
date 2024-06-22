k6 run --insecure-skip-tls-verify --out csv=grpcAllParks.csv grpcAllParks.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcPartialParks.csv grpcPartialParks.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcCreateUser.csv grpcCreateUser.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcAllParksClientStream.csv grpcAllParksClientStream.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcAllParksServerStream.csv grpcAllParksServerStream.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcAllParksTwoSidedStream.csv grpcAllParksTwoSidedStream.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcUpdateParkingValue.csv grpcUpdateParkingValue.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcLeavePark.csv grpcLeavePark.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcLeaveParkClientStream.csv grpcLeaveParkClientStream.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcLeaveParkServerStream.csv grpcLeaveParkServerStream.js
timeout /t 60
k6 run --insecure-skip-tls-verify --out csv=grpcLeaveParkTwoSideStream.csv grpcLeaveParkTwoSideStream.js