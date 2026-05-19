import { render, screen } from "@testing-library/react";
import "@testing-library/jest-dom";
import React from "react";

function Smoke() {
  return <div>Office Time</div>;
}

test("renders test environment", () => {
  render(<Smoke />);
  expect(screen.getByText("Office Time")).toBeInTheDocument();
});
