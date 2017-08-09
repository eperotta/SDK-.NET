/************* CONFIGURACION DEL API ************************/
window.TPFORMAPI.hybridForm.initForm({
    callbackValidationErrorFunction: 'validationCollector',
    callbackCustomSuccessFunction: 'customPaymentSuccessResponse',
    callbackCustomErrorFunction: 'customPaymentErrorResponse',
    callbackBilleteraFunction: 'billeteraPaymentResponse',
    botonPagarId: 'MY_btnConfirmarPago',
    botonPagarConBilleteraId: 'MY_btnPagarConBilletera',
    modalCssClass: 'modal-class',
    modalContentCssClass: 'modal-content',
    beforeRequest: 'initLoading',
    afterRequest: 'stopLoading'
});
/************* SETEO UN ITEM PARA COMPRAR ************************/
window.TPFORMAPI.hybridForm.setItem({
    publicKey: publicKey,
    defaultNombreApellido: completeName,
    defaultNumeroDoc: dni,
    defaultMail: mail,
    defaultTipoDoc: defDniType
});

//callbacks de respuesta del pago
function validationCollector(parametros) {
    console.log("My validator collector");
    console.log(parametros.field + " ==> " + parametros.error);
    console.log(parametros);
}
function billeteraPaymentResponse(response) {
    console.log("My wallet callback");
    console.log(response.ResultCode + " : " + response.ResultMessage);
    console.log(response);
    callResult(response);
}
function customPaymentSuccessResponse(response) {
    console.log("My custom payment success callback");
    console.log(response.ResultCode + " : " + response.ResultMessage);
    console.log(response);
    callResult(response);
}
function customPaymentErrorResponse(response) {
    console.log("Mi custom payment error callback");
    console.log(response.ResultCode + " : " + response.ResultMessage);
    console.log(response);
    callResult(response);
}
function initLoading() {
    console.log('Cargando');
}
function stopLoading() {
    console.log('Stop loading...');
}
function callResult(response) {
    $('#AnswerKey').val(response.AuthorizationKey);
    $('#StatusCode').val(response.ResultCode);
    $('#json').val(JSON.stringify(response));
    $('#FormResult').submit();
}