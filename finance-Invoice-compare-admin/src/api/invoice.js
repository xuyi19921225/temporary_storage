import request from '@/utils/request'

export function getSAPInvoiceList(params) {
  return request({
    url: '/invoice/getSAPInvoiceList',
    method: 'get',
    params
  })
}

export function addSAPInvoice(data) {
  return request({
    url: '/invoice/addSAPInvoice',
    method: 'post',
    data
  })
}

export function addSiteInvoice(data) {
  return request({
    url: '/invoice/addSiteInvoice',
    method: 'post',
    data
  })
}

export function getSiteInvoiceList(params) {
  return request({
    url: '/invoice/getSiteInvoiceList',
    method: 'get',
    params
  })
}

export function saveInvoice(data) {
  return request({
    url: '/invoice/saveInvoice',
    method: 'put',
    data
  })
}

