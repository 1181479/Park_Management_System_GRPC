import grpc from "k6/net/grpc";
import { Stream } from "k6/net/grpc";
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
  summaryTimeUnit: "ms",
  stages: [{ duration: "1m", target: 1 }],
};

export default () => {
  client.connect("localhost:7000", {});

  // Create the payload with the fields and field mask
  const req = { fieldMask: "parks" };
  const stream = new Stream(client, "Proto.ParkGrpc/GetAllParksServerStream");

  stream.on("end", function () {
    // The server has finished sending
    client.close();
    console.log("All done");
  });

  // send a message to the server
  stream.write(req);
};

export function handleSummary(data) {
  return {
    "grpcAllParksServerStream.html": htmlReport(data),
  };
}
