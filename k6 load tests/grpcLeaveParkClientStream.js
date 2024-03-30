import grpc from "k6/net/grpc";
import { check } from "k6";
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

const paymentClient = new grpc.Client();
const backofficeClient = new grpc.Client();
paymentClient.load(
  [
    // "../Park20.Backoffice.Api/Park20.Backoffice.Api/Protos/Services",
    // "../Park20.Backoffice.Api/Park20.Backoffice.Api/Protos/",
    "../PaymentSimulation/PaymentSimulation/Protos/Services",
    "../PaymentSimulation/PaymentSimulation/Protos/",
  ],
  "paymentgrpc.proto"
  // "vehiclegrpc.proto",
);

backofficeClient.load(
  [
    "../Park20.Backoffice.Api/Park20.Backoffice.Api/Protos/Services",
    "../Park20.Backoffice.Api/Park20.Backoffice.Api/Protos/",
    // "../PaymentSimulation/PaymentSimulation/Protos/Services",
    // "../PaymentSimulation/PaymentSimulation/Protos/",
  ],
  // "paymentgrpc.proto",
  "vehiclegrpc.proto"
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

export function setup() {
  paymentClient.connect("localhost:7004", {});

  // Create the payload with the fields and field mask
  const req = {
    token: "tok_123456789",
    fieldMask: "result",
  };

  const response = paymentClient.invoke("Protos.PaymentGrpc/AddToken", req);

  check(response, {
    "status is OK": (r) => r && r.status === grpc.StatusOK,
  });

  paymentClient.close();
}

export default () => {
  backofficeClient.connect("localhost:7000", {});

  // Create the payload with the fields and field mask
  const req = {
    isEntrance: false,
    licensePlate: "ABC123",
    parkName: "Central Park",
    fieldMask: "isSuccessfull,parkyCoinsAmount,otherPaymentMethodAmount,totalCost",
  };

  const response = backofficeClient.invoke("Proto.VehicleGrpc/LeavePark", req);

  check(response, {
    "status is OK": (r) => r && r.status === grpc.StatusOK,
  });

  paymentClient.close();
};

export function handleSummary(data) {
  return {
    "grpcLeaveParkClientStream.html": htmlReport(data),
  };
}
