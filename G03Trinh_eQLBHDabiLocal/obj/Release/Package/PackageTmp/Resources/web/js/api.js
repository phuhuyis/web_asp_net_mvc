$('.logout').off('click').on('click', function (e) {
    e.preventDefault();
    $.ajax({
        url: "/api/auth/logout",
        dataType: "json",
        method: "POST",
        contentType: "application/json; charset=utf-8",
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Đăng xuất thành công!",
                icon: "success"
            }).then((value) => {
                document.location.href = document.location.href;
                getCountCart();
            })
        },
        error: (rs) => {
            console.log(rs);
        }
    });
    getCountCart();
});
$('.add-cart-modal').off('click').on('click', function (e) {
    e.preventDefault();
    var btn = $(this);
    var id = btn.attr('data-id');
    var size = $('#size').val();
    $.ajax({
        url: "/api/cart",
        dataType: "json",
        method: "POST",
        data: {
            product: id,
            quantity: 1,
            size: size
        },
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Thêm vào giỏ hàng thành công!",
                icon: "success"
            }).then((value) => {
                document.location.href = document.location.href;
            })
        },
        error: (rs) => {
            if (rs.status == 401) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Vui lòng đăng nhập trước khi thêm sản phẩm vào giỏ hàng!",
                    icon: "error"
                }).then((value) => {
                    document.location.href = "/login";
                    getCountCart();
                    $("#select-size").modal("hide");
                })
                
            } else
                console.log(rs);
        }
    });
    getCountCart();
    $("#select-size").modal("hide");
});
$('.add-cart-home').off('click').on('click', async function (e) {
    e.preventDefault();
    var btn = $(this);
    var id = btn.attr("data-id");
    await $('.add-cart-modal').attr('data-id', id);
    $("#select-size").modal("show");
});
$('.add-cart').off('click').on('click', function (e) {
    e.preventDefault();
    var btn = $(this);
    let id = btn.data('id');
    let quantity = btn.attr('data-quantity');
    let size = btn.attr('data-size');
    let data = {
        product: id,
        quantity: quantity,
        size: size
    }
    console.log(JSON.stringify(data));
    console.log(data);
    console.log(btn);
    $.ajax({
        url: "/api/cart",
        dataType: "json",
        method: "POST",
        Headers: {
            "Content-Type": "application/json"
        },
        data: data,
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Thêm vào giỏ hàng thành công!",
                icon: "success"
            })
        },
        error: (rs) => {
            if (rs.status == 401) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Vui lòng đăng nhập trước khi thêm sản phẩm vào giỏ hàng!",
                    icon: "error"
                }).then((value) => {
                    document.location.href = "/login";
                    getCountCart();
                })
            }else
                console.log(rs);
        }
    });
    getCountCart();
});
$('.add-bill').off('click').on('click', function (e) {
    e.preventDefault();
    var btn = $(this);
    var id = btn.data('id');
    var quantity = btn.attr('data-quantity');
    var size = btn.attr('data-size');
    /*$.ajax({
        url: "/api/bill",
        dataType: "json",
        method: "POST",
        data: {
            product: id,
            quantity: quantity
        },
        success: (rs) => {
            document.getElementById('qr').setAttribute("src", rs);
            $("#myModal").modal("show");
            alert("Hóa đơn của bạn đã được chuyển đến người quản lý!");
        },
        error: (rs) => {
            if (rs.status == 401) {
                alert('Vui lòng đăng nhập trước khi thanh toán!');
                document.location.href = "/login";
            } else
                console.log(rs);
        }
    });
    getCountCart();*/
    $('#infoBill').modal("show");
});

$('.add-bill-no').off('click').on('click', function (e) {
    e.preventDefault();
    Swal.fire({
        title: "Lỗi",
        text: "Vui lòng đăng nhập trước khi thực hiện!",
        icon: "error"
    }).then((value) => {
        document.location.href = "/login";
    })
});

//cart
async function delAll() {
    return $.ajax({
        url: "/api/cart/all",
        dataType: "json",
        method: "DELETE",
        success: (rs) => {
            return true;
        },
        error: (rs) => {
            if (rs.status == 401) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Vui lòng đăng nhập trước khi thực hiện!",
                    icon: "error"
                }).then((value) => {
                    document.location.href = "/login";
                })
            } else
                console.log(rs);
            return false;
        }
    });
}

async function update(product, size, quantity) {
    return $.ajax({
        url: "/api/cart",
        dataType: "json",
        data: {
            "product": product,
            "quantity": quantity,
            "size": size
        },
        method: "PUT",
        success: (rs) => {
            return true;
        },
        error: (rs) => {
            if (rs.status == 401) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Vui lòng đăng nhập trước khi thực hiện!",
                    icon: "error"
                }).then((value) => {
                    document.location.href = "/login";
                })
            } else
                console.log(rs);
            return false;
        }
    });
}

async function del(product, size, quantity) {
    return $.ajax({
        url: "/api/cart",
        dataType: "json",
        data: {
            "product": product,
            "quantity": quantity,
            "size": size
        },
        method: "DELETE",
        success: (rs) => {
            return true;
        },
        error: (rs) => {
            if (rs.status == 401) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Vui lòng đăng nhập trước khi thực hiện!",
                    icon: "error"
                }).then((value) => {
                    document.location.href = "/login";
                })
            } else
                console.log(rs);
            return false;
        }
    });
}

async function payUser() {
    var name = $("#name").val();
    var address = $("#address").val();
    var phone = $("#phone").val();
    var payment = $("#payment").val();
    return $.ajax({
        url: "/api/bill",
        dataType: "json",
        method: "POST",
        data: {
            name: name,
            address: address,
            phone: phone,
            payment: payment
        },
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Hóa đơn của bạn đã được chuyển đến người quản lý!",
                icon: "success"
            }).then((value) => {
                if (rs != "success") {
                    $("#infoBill").modal("hide");
                    document.getElementById('qr').setAttribute("src", rs);
                    $("#myModal").modal("show");
                } else {
                    document.location.href = document.location.href;
                }
                return rs;
            })
            
            
        },
        error: (rs) => {
            if (rs.status == 401) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Vui lòng đăng nhập trước khi thực hiện!",
                    icon: "error"
                }).then((value) => {
                    document.location.href = "/login";
                })
            } else
                console.log(rs);
            return false;
        }
    });
}

async function pay(id, quantity, size) {
    var name = $("#name").val();
    var address = $("#address").val();
    var phone = $("#phone").val();
    var payment = $("#payment").val();
    return $.ajax({
        url: "/api/bill/create",
        dataType: "json",
        method: "POST",
        data: {
            name: name,
            address: address,
            phone: phone,
            payment: payment,
            product: id,
            quantity: quantity,
            size: size
        },
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Hóa đơn của bạn đã được chuyển đến người quản lý!",
                icon: "success"
            }).then((value) => {
                if (rs != "success") {
                    $("#infoBill").modal("hide");
                    document.getElementById('qr').setAttribute("src", rs);
                    $("#myModal").modal("show");
                } else {
                    document.location.href = document.location.href;
                }
                return rs;
            })
        },
        error: (rs) => {
            if (rs.status == 401) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Vui lòng đăng nhập trước khi thực hiện!",
                    icon: "error"
                }).then((value) => {
                    document.location.href = "/login";
                })
            } else
                console.log(rs);
            return false;
        }
    });
}

$('#btn-del-all-cart').off('click').on('click', async function (e) {
    e.preventDefault();
    Swal.fire({
        title: "Xác nhận",
        text: "Bạn có chắc chắn muốn xóa tất cả sản phẩm khỏi giỏ hàng hay không?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "OK"
    }).then(async (result) => {
        if (result.isConfirmed) {
            var rsDel = await delAll();
            if (rsDel) {
                $('#root-cart').html('<div class="col"><div class="h2">Giỏ hàng</div><div class="h3 text-danger mt-3">Chưa có sản phẩm nào trong giỏ hàng</div></div>');
            }
            getCountCart();
        }
    });
})

$('.del-cart').off('click').on('click', async function (e) {
    e.preventDefault();
    var btn = $(this);
    Swal.fire({
        title: "Xác nhận",
        text: "Bạn có chắc chắn muốn xóa sản phẩm này khỏi giỏ hàng hay không?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "OK"
    }).then(async (result) => {
        if (result.isConfirmed) {
            var rsDel = await del(btn.data('product'), btn.attr('data-size'), btn.attr('data-quantity'));
            if (rsDel) {
                document.location.href = document.location.href;
            }
        }
    });
    getCountCart();
})

$('.quantity-input').on('change', async function (e) {
    var input = $(this);
    if (input.val() <= 0) {
        Swal.fire({
            title: "Lỗi",
            text: "Số lượng phải lớn hơn 0!",
            icon: "error"
        }).then((value) => {
            document.location.href = document.location.href;
        })
    } else {
        var index = $('.quantity-input').index(this);
        $('.del-cart:eq(' + index + ')').attr('data-quantity', input.val());
        var rsUpdate = await update(input.data('product'), input.attr("data-size"), input.val());
        if (rsUpdate) {
            document.location.href = document.location.href;
        }
    }
})
$(document).on('click', '.number-spinner button', function () {
    var btn = $(this),
        oldValue = btn.closest('.number-spinner').find('input').val().trim(),
        newVal = 0;

    if (btn.attr('data-dir') == 'up') {
        newVal = parseInt(oldValue) + 1;
    } else {
        if (oldValue > 1) {
            newVal = parseInt(oldValue) - 1;
        } else {
            newVal = 1;
        }
    }
    btn.closest('.number-spinner').find('input').val(newVal);
    $('.add-cart').attr('data-quantity', $('#quantity').val());
    $('.add-bill').attr('data-quantity', $('#quantity').val());
    $('#billProduct').attr('data-quantity', $('#quantity').val());
});

$(document).on('click', '.number-spinner-size button', function () {
    var btn = $(this),
        oldValue = btn.closest('.number-spinner-size').find('input').val().trim(),
        newVal = oldValue;

    if (btn.attr('data-dir') == 'up') {
        if (oldValue < 42)
            newVal = parseInt(oldValue) + 1;
    } else {
        if (oldValue > 35) {
            newVal = parseInt(oldValue) - 1;
        } else {
            newVal = 35;
        }
    }
    btn.closest('.number-spinner-size').find('input').val(newVal);
    $('.add-cart').attr('data-size', $('#size').val());
    $('.add-bill').attr('data-size', $('#size').val());
    $('#billProduct').attr('data-size', $('#size').val());
});

$('#btn-pay').off('click').on('click', async function (e) {
    e.preventDefault();
    //await pay();
    //getCountCart();
    $('#infoBill').modal("show");
})

$('#bill').off('click').on('click', async function (e) {
    e.preventDefault();
    if (validateName($("#name").val()) != "") {
        Swal.fire({
            title: "Lỗi",
            text: validateName($("#name").val()),
            icon: "error"
        })
    }
    else
        if (validatePhone($("#phone").val()) != "") {
            Swal.fire({
                title: "Lỗi",
                text: validatePhone($("#phone").val()),
                icon: "error"
            })
        }
        else
            if (validateAddress($("#address").val()) != "") {
                Swal.fire({
                    title: "Lỗi",
                    text: validateAddress($("#address").val()),
                    icon: "error"
                })
            }
            else {
                await payUser();
                getCountCart();
            }
})

$('#billProduct').off('click').on('click', async function (e) {
    e.preventDefault();
    var btn = $(this);
    var id = btn.data('id');
    var quantity = btn.attr('data-quantity');
    var size = btn.attr('data-size');
    if (validateName($("#name").val()) != "") {
        Swal.fire({
            title: "Lỗi",
            text: validateName($("#name").val()),
            icon: "error"
        })
    }
    else
        if (validatePhone($("#phone").val()) != "") {
            Swal.fire({
                title: "Lỗi",
                text: validatePhone($("#phone").val()),
                icon: "error"
            })
        }
        else
            if (validateAddress($("#address").val()) != "") {
                Swal.fire({
                    title: "Lỗi",
                    text: validateAddress($("#address").val()),
                    icon: "error"
                })
            }
            else {
                await pay(id, quantity, size);
                getCountCart();
            }
})

const validatePhone = (text) => {
    if (text == null || text == undefined || text.length == 0) {
        return "Vui lòng nhập số điện thoại";
    }
    var regex = /^(0[3|5|7|8|9])+([0-9]{8})\b$/i;
    if (text.match(regex))
        return "";
    return "SĐT không hợp lệ, phải gồm 10 chữ số và bắt đầu bằng 03, 05, 07, 08 hoặc 09";
}

const validateName = (text) => {
    if (text == null || text == undefined || text.length == 0) {
        return "Vui lòng nhập họ và tên";
    }
    return "";
}

const validateAddress = (text) => {
    if (text == null || text == undefined || text.length == 0) {
        return "Vui lòng nhập họ và tên";
    }
    return "";
}

function getCountCart() {
    $.ajax({
        url: "/api/cart/count",
        dataType: "json",
        method: "GET",
        success: (rs) => {
            console.log(rs);
            $('#cart-count').html(rs.count);
        },
        error: (rs) => {
            console.log(rs);
            $('#cart-count').html(0);
        }
    });
}

getCountCart();

function loadPage() {
    document.location.href = document.location.href;
}