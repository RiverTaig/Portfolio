function Test() {
    BootstrapDialog.show({
        title: 'Default Title',
        message: 'Click buttons below.',
        buttons: [{
            label: 'Message 1',
            action: function (dialog) {
                dialog.setMessage('Message 1');
            }
        }, {
            label: 'Message 2',
            action: function (dialog) {
                dialog.setMessage('Message 2');
            }
        }]
    });
}