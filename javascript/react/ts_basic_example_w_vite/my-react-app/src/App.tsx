import { useState, useEffect, /* useTransition, */ lazy } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import data from './assets/data.json'
import DataWidget from './DataWidget'
import doThing from './doThing'

const Counter = lazy(() => import("./Counter"));
const Admin = lazy(() => import("./Admin"));

function App() {
  // const [, startTransition] = useTransition();
  const [counterLoad, setCounterLoad] = useState(false);
  const [adminLoad, setAdminLoad] = useState(false);
  const [active, setActive] = useState(false);

  function WidgetLoad() {
    console.log(import.meta.env.MODE)
    data.Widgets.forEach((widget) => {
      if (widget === "Admin") {
        console.log("setting admin load to true")
        setAdminLoad(true);
      }
      if (widget === "Counter") {
        console.log("setting counter load to true")
        setCounterLoad(true);
      }
    });
  }

  useEffect(() => { WidgetLoad(); }, []);

  function handleClick() {
    setActive(!active);
  }

  return (
    <>
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://react.dev" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      {/* <button onClick={() => startTransition(() => { setCounterLoad(!counterLoad);})}>Toggle Counter</button> */}
      <div className="card">
        {counterLoad && <Counter />}
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      {/* <button onClick={() => startTransition(() => { setAdminLoad(!adminLoad);})}>Toggle Admin</button> */}
      {adminLoad && <Admin />}
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
      <p>
        This is a test of the emergency broadcast system
      </p>

      <p style={{ color: active ? "red" : "white"}}>
        No idea what's going on
      </p>
      <button onClick={() => handleClick() }>Make Text Red</button>
      <DataWidget />

      <button onClick={() => doThing()}>Click to Do Things</button>
    </>
  )
}

export default App
