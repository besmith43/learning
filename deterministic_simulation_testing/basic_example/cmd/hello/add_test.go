package main

import "testing"

func TestAdd(t *testing.T) {
	testcases := []struct {
		x, y, expectedResult int32
	}{
		{1, 2, 3},
		{5, 2, 7},
		{10, 2, 12},
		{15, 30, 45},
		{18, 4, 22},
	}

	for _, tt := range testcases {
		result := add(tt.x, tt.y)

		if result != tt.expectedResult {
			t.Errorf("expected=%d, got=%d", tt.expectedResult, result)
		}
	}
}
