class CounterComponent extends HTMLElement  {
    constructor() {
        super();
        this.count = 0;
    }

    render() {
        this.innerHTML = `
            <section>
                <p>Counter Web Component</p>
                <p>Count: <span>${this.count}</span></p>
                <button id="decrement">-</button>
                <button id="increment">+</button>
            </section>
        `;

        // this loads and looks good, until you click the buttons.  then it does nothing
        // turns out the the query selector is not finding the buttons
        this.oldInnerHTML = `
            <div hx-get='/templates/counter2.html' hx-trigger='load'></div>
        `;

        this.querySelector('#increment').addEventListener('click', () => this.increment());
        this.querySelector('#decrement').addEventListener('click', () => this.decrement());
    }

    increment() {
        console.log("increment");
        this.count++;
        this.querySelector('span').innerText = this.count;
    }

    decrement() {
        console.log("decrement");
        this.count--;
        this.querySelector('span').innerText = this.count;
    }

    connectedCallback() {
        console.log("CounterComponent added to page.");
        this.render();
    }
}

customElements.define('counter-component', CounterComponent);
