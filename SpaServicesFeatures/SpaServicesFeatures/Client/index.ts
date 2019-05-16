
export class MySampleClass {

    constructor() {

    }

    showAlert(text: string) {
        window.alert('Sample Class Alert: ' + text);
    }
}

(<any>window).Sample = new MySampleClass();
