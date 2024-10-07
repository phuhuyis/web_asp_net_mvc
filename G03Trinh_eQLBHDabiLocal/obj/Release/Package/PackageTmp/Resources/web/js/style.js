$('#quantity').off('change').on('change', function (e) {
    var input = $(this);
    if (input.val() <= 0) {
        Swal.fire({
            title: "Lỗi",
            text: "Số lượng phải lớn hơn 0!",
            icon: "error"
        }).then((value) => {
            input.val($('.add-cart').attr('data-quantity'));
        })
    } else {
        $('.add-cart').attr('data-quantity', $('#quantity').val());
        $('.add-bill').attr('data-quantity', $('#quantity').val());
        $('#billProduct').attr('data-quantity', $('#quantity').val());
    }
});
$('#size').off('change').on('change', function (e) {
    var input = $(this);
    if (input.val() > 42 || input.val() < 35) {
        Swal.fire({
            title: "Lỗi",
            text: "Size phải từ 35 đến 42!",
            icon: "error"
        }).then((value) => {
            input.val($('.add-cart').attr('data-size'));
        })
    } else {
        $('.add-cart').attr('data-size', $('#size').val());
        $('.add-bill').attr('data-size', $('#size').val());
        $('#billProduct').attr('data-size', $('#size').val());
    }
});