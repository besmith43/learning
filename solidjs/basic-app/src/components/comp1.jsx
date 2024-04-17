import { createSignal } from "solid-js";
import styles from './comp1.module.css';
import { Button } from 'solid-bootstrap';

const [count, setCount] = createSignal(0);

export default function Comp1() {

	function add() {
		setCount(count() + 1);
	}

	function remove() {
		setCount(count() - 1);
	}

  return (
	<div style="padding: 15px;">
	  <p>You clicked {count()} times</p>
	  <Button variant="primary" class={styles.button} onClick={() => add() }>Add</Button>
	  <Button variant="primary" onClick={() => remove() }>Remove</Button>
	</div>
  );
}



