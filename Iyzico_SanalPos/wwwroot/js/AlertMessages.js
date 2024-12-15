function alert(number, text, title) {
    switch (number) {
        case 1: // Success alert
            Swal.fire({
                icon: 'success',
                title: title || 'Başarılı!',
                text: text,
            });
            break;
        case 2: // Error alert
            Swal.fire({
                icon: 'error',
                title: title || 'Hata!',
                text: text,
            });
            break;

        default: 

    }
}
