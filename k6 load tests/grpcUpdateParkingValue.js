import grpc from "k6/net/grpc";
import { check } from "k6";
import { randomIntBetween } from "https://jslib.k6.io/k6-utils/1.2.0/index.js";
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

const client = new grpc.Client();
client.load(
  [
    "../Park20.Backoffice.Api/Park20.Backoffice.Api/Protos/Services",
    "../Park20.Backoffice.Api/Park20.Backoffice.Api/Protos/",
  ],
  "parkygrpc.proto"
);

// Define the load test configuration
export const options = {
  summaryTrendStats: [
    "avg", //average
    "min",
    "med", //median
    "max",
    "p(50)",
    "p(90)",
    "p(95)",
    "p(99)",
    "p(99.99)",
    "count",
  ],
  summaryTimeUnit: "ms",
  stages: [{ duration: "1m", target: 1 }],
};

export default () => {
  client.connect("localhost:7000", {});

  // Create the payload with the fields and field mask
  let value = randomIntBetween(0, 99999);
  const req = {
    amount: value,
    fieldMask: 'isSuccessful',
  };

  const response = client.invoke("Proto.ParkyGrpc/UpdateParkingValue", req);
  check(response, {
    "status is OK": (r) => r && r.status === grpc.StatusOK,
  });

  client.close();
};

export function handleSummary(data) {
  return {
    "grpcUpdateParkingValue.html": htmlReport(data),
  };
}
