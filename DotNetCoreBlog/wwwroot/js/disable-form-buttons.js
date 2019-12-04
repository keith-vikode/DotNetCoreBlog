const forms = document.querySelectorAll("form");
for (let i = 0; i < forms.length; i++) {
    const form = forms[i];
    form.addEventListener("submit", e => {
        // don't do anything if the form is invalid; let the browser deal with it
        if (form.checkValidity()) {
            // disable all form controls on form submit
            const controls = form.querySelectorAll("button,input,textarea,select");
            for (let j = 0; j < controls.length; j++) {
                const control = controls[i];
                // form controls set to readOnly, as disabling will prevent their values
                // being submitted; buttons should be disabled
                if (control instanceof HTMLInputElement ||
                    control instanceof HTMLTextAreaElement) {
                    control.readOnly = true;
                } else if (control instanceof HTMLButtonElement) {
                    control.disabled = true;
                }
            }
        }
    });
}