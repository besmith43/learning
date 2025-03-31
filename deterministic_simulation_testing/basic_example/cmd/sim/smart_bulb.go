package main

import "errors"

type Bulb struct {
	state      bool
	brightness int
}

func New() *Bulb {
	return &Bulb{state: false, brightness: 0}
}

func (b *Bulb) Toggle() {
	if b.state {
		b.state = false
	} else if b.state == false && b.brightness == 0 {
		b.state = true
		b.brightness = 100
	} else {
		b.state = true
	}
}

func (b *Bulb) SetBrightness(level int) error {
	if level == 0 {
		b.state = false
		b.brightness = 0
	} else if level > 100 {
		return errors.New("brightness can't be over 100")
	} else if level < 0 {
		return errors.New("brightness can't be negative")
	} else {
		b.state = true
		b.brightness = level
	}

	return nil
}

func (b *Bulb) GetBrightness() int {
	if b.state == false {
		return 0
	} else {
		return b.brightness
	}
}
