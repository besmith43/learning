import { useState, useEffect } from 'react'
import dataWidget from './assets/widgetData.json'


function DataWidget() {
    const [data, setData] = useState("");

   useEffect(() => {
        if (import.meta.env.DEV) {
            setData(dataWidget.FirstName);
        } else {
            setData("Peter");
        }
    }, []); 

    return (
        <>
        <div>
            <p>Hello {data}</p>
        </div>
        </>
    )
}


export default DataWidget