import grpc from "k6/net/grpc";
import { check } from "k6";
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

const client = new grpc.Client();
client.load(
  [
    "../Park20.Backoffice.Api/Park20.Backoffice.Api/Protos/Services",
    "../Park20.Backoffice.Api/Park20.Backoffice.Api/Protos/",
  ],
  "parkgrpc.proto"
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
  duration: '20m',
  summaryTimeUnit: "ms",
  iterations: 3000
};

export default () => {
  client.connect("localhost:7000", {});

  // Create the payload with the fields and field mask
  const req = { fieldMask: "parks" };

  const response = client.invoke("Proto.ParkGrpc/GetAllParks", req);

  check(response, {
    "status is OK": (r) => r && r.status === grpc.StatusOK,
  });

  client.close();
};

export function handleSummary(data) {
  return {
    "grpcAllParks.html": htmlReport(data),
  };
}
