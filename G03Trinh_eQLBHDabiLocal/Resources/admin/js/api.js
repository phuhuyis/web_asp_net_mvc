$('.btn-del-cus').off('click').on('click', function (e) {
    e.preventDefault();
    Swal.fire({
        title: "Xác nhận",
        text: "Việc xóa khách hàng sẽ xóa toàn bộ dữ liệu liên quan đến khách hàng đó (giỏ hàng, đơn hàng, tài khoản,...) bạn có chắc chắn muốn xóa hay không?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "OK"
    }).then(async (result) => {
        if (result.isConfirmed) {
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/api/admin/customer/" + id,
                dataType: "json",
                method: "DELETE",
                success: (rs) => {
                    Swal.fire({
                        title: "Thông báo",
                        text: "Xóa khách hàng thành công!",
                        icon: "success"
                    }).then((value) => {
                        document.location.href = "/admin/customer/list";
                    });
                },
                error: (rs) => {
                    if (rs.status == 404) {
                        Swal.fire({
                            title: "Lỗi",
                            text: "Khách hàng không tồn tại!",
                            icon: "error"
                        });
                    }
                }
            });
        }
    });
});
$('.btn-del-category').off('click').on('click', function (e) {
    e.preventDefault();
    Swal.fire({
        title: "Xác nhận",
        text: "Việc xóa hãng sẽ xóa toàn bộ dữ liệu liên quan đến hãng đó (giỏ hàng, đơn hàng, túi sách) bạn có chắc chắn muốn xóa hay không?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "OK"
    }).then(async (result) => {
        if (result.isConfirmed) {
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/api/admin/category/" + id,
                dataType: "json",
                method: "DELETE",
                success: (rs) => {
                    Swal.fire({
                        title: "Thông báo",
                        text: "Xóa hãng thành công!",
                        icon: "success"
                    }).then((value) => {
                        document.location.href = "/admin/category/list";
                    });
                },
                error: (rs) => {
                    if (rs.status == 404) {
                        Swal.fire({
                            title: "Lỗi",
                            text: "Hãng không tồn tại!",
                            icon: "error"
                        });
                    }
                }
            });
        }
    });
});
$('.btn-del-product').off('click').on('click', function (e) {
    e.preventDefault();
    Swal.fire({
        title: "Xác nhận",
        text: "Việc xóa túi sách sẽ xóa toàn bộ dữ liệu liên quan đến túi sách đó (giỏ hàng, đơn hàng) bạn có chắc chắn muốn xóa hay không?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "OK"
    }).then(async (result) => {
        if (result.isConfirmed) {
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/api/admin/product/" + id,
                dataType: "json",
                method: "DELETE",
                success: (rs) => {
                    Swal.fire({
                        title: "Thông báo",
                        text: "Xóa túi sách thành công!",
                        icon: "success"
                    }).then((value) => {
                        document.location.href = "/admin/product/list";
                    });
                },
                error: (rs) => {
                    if (rs.status == 404) {
                        Swal.fire({
                            title: "Lỗi",
                            text: "Túi sách không tồn tại!",
                            icon: "error"
                        });
                    }
                }
            });
        }
    });
});
$('.btn-del-product').off('click').on('click', function (e) {
    e.preventDefault();
    Swal.fire({
        title: "Xác nhận",
        text: "Việc xóa túi sách sẽ xóa toàn bộ dữ liệu liên quan đến túi sách đó (giỏ hàng, đơn hàng) bạn có chắc chắn muốn xóa hay không?",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "OK"
    }).then(async (result) => {
        if (result.isConfirmed) {
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/api/admin/product/" + id,
                dataType: "json",
                method: "DELETE",
                success: (rs) => {
                    Swal.fire({
                        title: "Thông báo",
                        text: "Xóa túi sách thành công!",
                        icon: "success"
                    }).then((value) => {
                        document.location.href = "/admin/product/list";
                    });
                },
                error: (rs) => {
                    if (rs.status == 404) {
                        Swal.fire({
                            title: "Lỗi",
                            text: "Túi sách không tồn tại!",
                            icon: "error"
                        });
                    }
                }
            });
        }
    });
});
$('.handle-contact').off('click').on('click', function (e) {
    e.preventDefault();
    var btn = $(this);
    $.ajax({
        url: "/api/admin/contact/" + btn.data('id'),
        dataType: "json",
        method: "PUT",
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Xử lý thông tin liên hệ thành công!",
                icon: "success"
            }).then((value) => {
                document.location.href = "/admin/contact";
            });
        },
        error: (rs) => {
            if (rs.status == 404) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Thông tin liên hệ không tồn tại!",
                    icon: "error"
                });
            }
        }
    });
})
$('.no-handle-contact').off('click').on('click', function (e) {
    e.preventDefault();
    var btn = $(this);
    $.ajax({
        url: "/api/admin/contact/" + btn.data('id'),
        dataType: "json",
        method: "DELETE",
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Đã xóa trạng thái thông tin liên hệ thành công!",
                icon: "success"
            }).then((value) => {
                document.location.href = "/admin/contact";
            });
        },
        error: (rs) => {
            if (rs.status == 404) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Thông tin liên hệ không tồn tại!",
                    icon: "error"
                });
            }
        }
    });
})
$('.handle-bill').off('click').on('click', function (e) {
    e.preventDefault();
    var btn = $(this);
    $.ajax({
        url: "/api/admin/bill/" + btn.data('id'),
        dataType: "json",
        method: "PUT",
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Xử lý thông tin đơn hàng thành công!",
                icon: "success"
            }).then((value) => {
                document.location.href = "/admin/bill";
            });
        },
        error: (rs) => {
            if (rs.status == 404) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Thông tin đơn hàng không tồn tại!",
                    icon: "error"
                });
            }
        }
    });
})
$('.no-handle-bill').off('click').on('click', function (e) {
    e.preventDefault();
    var btn = $(this);
    $.ajax({
        url: "/api/admin/bill/" + btn.data('id'),
        dataType: "json",
        method: "DELETE",
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Đã xóa trạng thái xử lý thông tin đơn hàng thành công!",
                icon: "success"
            }).then((value) => {
                document.location.href = "/admin/bill";
            });
        },
        error: (rs) => {
            if (rs.status == 404) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Thông tin đơn hàng không tồn tại!",
                    icon: "error"
                });
            }
        }
    });
})
$('.handle-bill-home').off('click').on('click', function (e) {
    e.preventDefault();
    var btn = $(this);
    $.ajax({
        url: "/api/admin/bill/" + btn.data('id'),
        dataType: "json",
        method: "PUT",
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Xử lý thông tin đơn hàng thành công!",
                icon: "success"
            }).then((value) => {
                document.location.href = "/admin";
            });
        },
        error: (rs) => {
            if (rs.status == 404) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Thông tin đơn hàng không tồn tại!",
                    icon: "error"
                });
            }
        }
    });
})
$('.no-handle-bill-home').off('click').on('click', function (e) {
    e.preventDefault();
    var btn = $(this);
    $.ajax({
        url: "/api/admin/bill/" + btn.data('id'),
        dataType: "json",
        method: "DELETE",
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Đã xóa trạng thái xử lý thông tin đơn hàng thành công!",
                icon: "success"
            }).then((value) => {
                document.location.href = "/admin";
            });
        },
        error: (rs) => {
            if (rs.status == 404) {
                Swal.fire({
                    title: "Lỗi",
                    text: "Thông tin đơn hàng không tồn tại!",
                    icon: "error"
                });
            }
        }
    });
})
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
            });
        },
        error: (rs) => {
            console.log(rs);
        }
    });
});
$('.actual-btn').on('change', function () {
    var actualBtn = $(this);
    var form_data = new FormData();
    form_data.append("id", actualBtn.data('id'));
    form_data.append("image", actualBtn[0].files[0]);
    console.log(form_data);
    $.ajax({
        url: "/api/admin/slide",
        dataType: "json",
        processData: false,
        contentType: false,
        data: form_data,
        method: "POST",
        success: (rs) => {
            Swal.fire({
                title: "Thông báo",
                text: "Cập nhập thành công!",
                icon: "success"
            }).then((value) => {
                document.location.href = document.location.href;
            });
        },
        error: (rs) => {
            if (rs.status == 400) {
                Swal.fire({
                    title: "Lỗi",
                    text: "File không hợp lệ!",
                    icon: "error"
                });
            }
            if (rs.status == 415) {
                Swal.fire({
                    title: "Lỗi",
                    text: rs.responseJSON,
                    icon: "error"
                });
            }
        }
    });
});
$('.btn-update-slide').off('click').on('click', function (e) {
    e.preventDefault();
    const btn = $(this);
    var i = btn.data('icon');
    const actualBtn = document.getElementsByClassName('actual-btn')[i - 1];
    actualBtn.click();
});