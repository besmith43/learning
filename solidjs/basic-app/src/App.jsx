import logo from './logo.svg';
import styles from './App.module.css';
import Comp1 from './components/comp1';

function App() {
  return (
    <div class={styles.App}>
      <header class={styles.header}>
        <img src={logo} class={styles.logo} alt="logo" />
        <p style="z-index: 1;">
          Edit <code>src/App.jsx</code> and save to reload.
        </p>
        <a
          class={styles.link}
          href="https://github.com/solidjs/solid"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn Solid
        </a>

        <div>
          { Comp1() }
        </div>
      </header>
    </div>
  );
}

export default App;
