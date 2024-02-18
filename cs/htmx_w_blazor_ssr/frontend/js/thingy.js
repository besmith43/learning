class ThingyComponent extends HTMLElement {
    constructor() {
        super();
        this.textContent = "Thingy Web Component Text";
    }

    connectedCallback() {
        this.render();
    }

    render() {
        this.innerHTML = `
            <section>
                <p>Thingy Web Component</p>
                <p>Count: <span>${this.textContent}</span></p>
                <button id="thingy">update</button>
            <section>
        `;

        this.querySelector('#thingy').addEventListener('click', () => this.textContent = "Thingy Web Component Text Updated");
    }
}

// Register the CurrentDate component using the tag name <current-date>.
customElements.define('thingy-component', ThingyComponent );