function ShowImagePreview(ImageUploader, previewImage) {
    if (ImageUploader.files && ImageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(ImageUploader.files[0]);
    }
}


function jQueryAjaxPost(form)
{
    $.validator.unobtrusive.parse(form);
    
    if ($(form).valid())
    {
        var ajaxConfig = {

            type: 'POST',
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    Popup.dialog('close');
                    datatable.ajax.reload();

                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "success"
                    })

                }
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data"){
            ajaxConfig["contentType"] = true;
            ajaxConfig["processData"] = true;
        }
        $.ajax(ajaxConfig);
    }

    return false;
}


function jQueryAjaxPost444(form) {
    $.validator.unobtrusive.parse(form);

    if ($(form).valid()) {
        var ajaxConfig = {

            type: 'POST',
            url: form.action,
            data: new FormData(form),
            success: function (response) {
                if (response.success) {
                    Popup.dialog('close');
                    datatable.ajax.reload();

                    $.notify(response.message, {
                        globalPosition: "top center",
                        className: "success"
                    })
                }
            }
        }
        if ($(form).attr('enctype') == "multipart/form-data") {
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
        }
        $.ajax(ajaxConfig);
    }

    return false;
}