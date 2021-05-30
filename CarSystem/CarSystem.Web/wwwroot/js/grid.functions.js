function imageItem(item) {
    if (item) {
        return "<img src='" + item + "' class='img-circle' style='width:50px'/>";
    } else {
        return "";
    }
}

function formatTimeSpan(item) {
    if (item)
        return kendo.toString(item.Hours, "00") + ":" + kendo.toString(item.Minutes, "00") + ":" + kendo.toString(item.Seconds, "00") + "." + kendo.toString(item.Milliseconds, "000");
    else
        return "";
}

function booleanItem(isTrue) {
    if (isTrue)
        return "<i class='fa fa-check text-success'/>";
    return "";
}

function booleanItemCheck(isTrue) {
    if (isTrue)
        return "<i class='fa fa-check text-success'></i>";
    else
        return "<i class='fa fa-times text-danger'></i>";
}

function isInactive(isTrue) {
    if (isTrue)
        return "<span class='badge badge-danger'>Inativo</span>";
    else
        return "<span class='badge badge-success'>Ativo</span>";
}

function checkItem(isChecked) {
    var checked = isChecked ? "checked" : "";
    return "<input type='checkbox' value='" + isChecked + "' " + checked + ">";
}

function setDate(item, format) {
    return kendo.format('{0:' + format + '}', kendo.parseDate(item));
}

function customAction(item, action, text) {
    var html = kendo.format("<a class=\"btn btn-sm btn-default\" href=\"{0}/{1}\">{2}</a>", action, item.Id, text);
    return html;
}

function editItem(item, action) {
    var html = kendo.format("<a class=\"btn btn-sm btn-warning\" href=\"{0}/{1}\"><i class=\"fa fa-pencil\"></i></a>", action, item.Id);
    return html;
}

function detailItem(item, action) {
    var html = kendo.format("<a class=\"btn btn-sm btn-info\" href=\"{0}/{1}\"><i class=\"fa fa-file-text\"></i></a>", action, item.Id);
    return html;
}

function deleteItem(item, action) {
    var html = kendo.format("<a class=\"btn btn-sm btn-danger\" href=\"javascript:deleteItemGrid('{0}', '{1}')\"><i class=\"fa fa-times\"></i></a>", action, item.Id);
    return html;
}

function editAndDeleteItem(item, actionEdit, actionDelete) {
    var html = kendo.format('<div class="btn-group btn-group-sm" role="group">' +
        '<a href="{1}/{0}" class="btn btn-warning"><i class="fa fa-edit"></i></a>' +
        '<a href="javascript:deleteItemGrid(\'{2}\', \'{0}\')" class="btn btn-danger"><i class="fa fa-times"></i></a>' +
        '</div>', item.Id, actionEdit, actionDelete);
    return html;
}

function editAndDeleteItemJS(item, editJsFunction, actionDelete, gridName) {
    var html = kendo.format('<div class="btn-group btn-group-sm" role="group">' +
        '<a href="javascript:{1}(\'{0}\')" class="btn btn-warning"><i class="fa fa-edit"></i></a>' +
        '<a href="javascript:deleteItemGrid(\'{2}\', \'{0}\', \'{3}\')" class="btn btn-danger"><i class="fa fa-times"></i></a>' +
        '</div>',
        item.Id,
        editJsFunction,
        actionDelete,
        gridName);
    return html;
}

function GetThumbnailUrl(fileUrl, width, height) {
    var url = "";
    if (fileUrl) {
        var fileName = fileUrl.split(/(\\|\/)/g).pop();
        var filePath = fileUrl.substring(0, fileUrl.lastIndexOf(fileName));
        url = `${filePath}${width}x${height}/${fileName}`;
    }
    return url;
}

function deleteItemGrid(action, id, gridName) {

    if (!gridName)
        gridName = "Grid";

    Swal.fire({
        title: 'Confirma a exclusão?',
        text: "Essa ação não poderá ser revertida!",
        icon: 'question',
        showCancelButton: true,
        confirmButtonClass: 'btn btn-primary',
        cancelButtonClass: 'btn btn-danger m-r-md',
        confirmButtonText: 'Sim',
        cancelButtonText: 'Não',
        reverseButtons: true,
        buttonsStyling: false
    }).then((result) => {
        if (result.isConfirmed){
            $.ajax({
                url: action,
                type: 'POST',
                data: jQuery.param({ id: id }),
                contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                success: function (response) {
                    if (response === "" || response.result === true) {

                        Swal.fire({
                            title: 'Ok!',
                            text: 'Seu registro foi excluido.',
                            icon: 'success'
                        }).then(function () {
                            var grid = $("#" + gridName).data("kendoGrid");
                            grid.dataSource.read();
                        });
                    }
                    else if (response.result === false && response.message) {
                        Swal.fire(
                            'Não permitido!',
                            response.message,
                            'error'
                        );
                    } else {
                        Swal.fire(
                            'Não permitido!',
                            'Ocorreu um erro ao excluir o registro.',
                            'error'
                        );
                    }
                },
                error: function () {
                    Swal.fire(
                        'Erro!',
                        'Ocorreu um erro ao excluir o registro.',
                        'error'
                    );
                }
            });
        }
    });
}

function saveAsExcel(grid) {
    $("#" + grid).getKendoGrid().saveAsExcel();
}

function getOrderStatus(orderStatus) {
    switch (orderStatus) {
        case 1:
            return '<span class="badge badge-warning"><i class="fa fa-clock-o" ></i>&nbsp;Aguardando Pagamento</span>';
        case 2:
            return '<span class="badge badge-success"><i class="fa fa-thumbs-o-up"></i>&nbsp;Pago</span>';
        case 3:
            return '<span class="badge accent"><i class="fa fa-cube"></i>&nbsp;Separando Pedido</span>';
        case 4:
            return '<span class="badge deep-purple"><i class="fa fa-file-text-o"></i>&nbsp;Nota Fiscal Emitida</span>';
        case 5:
            return '<span class="badge badge-info"><i class="fa fa-truck"></i>&nbsp;Enviado</span>';
        case 6:
            return '<span class="badge dark"><i class="fa fa-check"></i>&nbsp;Entregue</span>';
        case 7:
            return '<span class="badge badge-danger"><i class="fa fa-remove"></i>&nbsp;Cancelado</span>';
        case 8:
            return '<span class="badge badge-info"><i class="fa fa-archive"></i>&nbsp;Arquivado</span>';
        default:
            return '<span class="badge grey-200 text-white"><i class="fa fa-question"></i>&nbsp;Não concluído</span>';
    }
}

function getPaymentMethod(paymentMethods) {
    var html = "";
    if (paymentMethods) {
        $.each(paymentMethods.split(";"), function (index, obj) {
            switch (obj.trim()) {
                case "Uninformed":
                    html += ' <i class="fa fa-question-circle-o"></i>';
                    break;
                case "CreditCard":
                    html += ' <i class="fa fa-credit-card"></i>';
                    break;
                case "Boleto":
                    html += ' <i class="fa fa-barcode"></i>';
                    break;
                case "DebitCard":
                    html += ' <i class="fa fa-credit-card-alt"></i>';
                    break;
                case "Cash":
                    html += ' <i class="fa fa-money"></i>';
                    break;
                case "Credits":
                    html += ' <i class="fa fa-creative-commons"></i>';
                    break;
                case "BankDeposit":
                    html += ' <i class="fa fa-university"></i>';
                    break;
                case "MachineCard":
                    html += ' <i class="fa fa-mobile"></i>';
                    break;
            }
        });
    }

    return html;
}

function getTransactionStatus(transactionStatus) {
    var color = "";
    var text = "";
    if (transactionStatus != null) {
        switch (transactionStatus) {
            case 0:
                color = 'secondary';
                text = 'Nenhum';
                break;
            case 1:
                color = 'warning';
                text = 'Autorizado';
                break;
            case 2:
                color = 'success';
                text = 'Pago';
                break;
            case 3:
                color = 'danger';
                text = 'Pendente de Reembolo';
                break;
            case 4:
                color = 'warning';
                text = 'Processando';
                break;
            case 5:
                color = 'danger';
                text = 'Reembolsado';
                break;
            case 6:
                color = 'danger';
                text = 'Recusado';
                break;
            case 7:
                color = 'warning';
                text = 'Aguardando Pagamento';
                break;
            case 8:
                color = 'danger';
                text = 'Chargedback';
                break;
            case 9:
                color = 'danger';
                text = 'Revertido';
                break;
            case 10:
                color = 'danger';
                text = 'Cancelado';
                break;
            case 11:
                color = 'danger';
                text = 'Erro';
                break;
            case 12:
                color = 'danger';
                text = 'Falhou';
                break;
        }
    }

    return `<span class="badge ${color}">${text}</span>`;
}

function getSubscriptionStatus(subscriptionStatus) {
    var color = "";
    var text = "";
    if (subscriptionStatus != null) {
        switch (subscriptionStatus) {
        case 0:
            color = 'success';
            text = 'Ativo';
            break;
        case 1:
            color = 'danger';
            text = 'Cancelado';
            break;
        }
    }

    return `<span class="badge ${color}">${text}</span>`;
}

function getShippingMethod(data) {

    if (data.ShippingMethodType === 1 || data.IsProcessingOrder === false)
        return "";

    var style = "bg-success";

    if (data.GetPercentDelivery > 50) {
        style = "bg-danger";
    }
    else if (data.GetPercentDelivery > 30) {
        style = "bg-warning";
    }

    var days = Math.round((Date.now() - data.OrderDate) / (1000 * 60 * 60 * 24));

    var html = '<div style="width: 100%"><div class="progress progress-sm">' +
        '<div class="progress-bar ' + style + '" role="progressbar" style="width:' + data.GetPercentDelivery + '%" aria-valuenow="' + data.GetPercentDelivery + '" aria-valuemin="0" aria-valuemax="100">' +
        '<span class="text-white">' + days + ' de ' + data.DeliveryDeadline + '</span>' +
        '</div></div></div>';

    return html;
}