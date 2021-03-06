import request from '@/utils/request'

export function getSAPInvoiceList(params) {
  return request({
    url: '/invoice/getSAPInvoiceList',
    method: 'get',
    params
  })
}

export function getAllSAPInvoiceList(params) {
  return request({
    url: '/invoice/getAllSAPInvoiceList',
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

export function getAllSiteInvoiceList(params) {
  return request({
    url: '/invoice/getAllSiteInvoiceList',
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

export function matchInvoice(params) {
  return request({
    url: '/invoice/matchInvoice',
    method: 'get',
    params
  })
}

