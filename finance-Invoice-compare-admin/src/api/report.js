import request from '@/utils/request'

export function getMatchInvoiceReport(params) {
  return request({
    url: '/report/getMatchInvoiceReport',
    method: 'get',
    params
  })
}

export function getUnMatchInvoiceReport(params) {
  return request({
    url: '/report/getUnMatchInvoiceReport',
    method: 'get',
    params
  })
}

export function getCompareMatchInvoiceReport(params) {
  return request({
    url: '/report/getCompareMatchInvoiceReport',
    method: 'get',
    params
  })
}

export function getAllCompareMatchInvoiceReport(params) {
  return request({
    url: '/report/getAllCompareMatchInvoiceReport',
    method: 'get',
    params
  })
}

export function addPayment(data) {
  return request({
    url: '/report',
    method: 'post',
    data
  })
}

