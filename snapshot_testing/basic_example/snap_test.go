package basic

import (
	"testing"

	"github.com/gkampitakis/go-snaps/snaps"
)

func TestExample(t *testing.T) {
	snaps.MatchSnapshot(t, "Hello World")
}
