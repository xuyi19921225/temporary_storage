import request from '@/utils/request'

export function getTaxCodeList(params) {
  return request({
    url: '/taxcode',
    method: 'get',
    params
  })
}

export function addTaxCode(data) {
  return request({
    url: '/taxcode',
    method: 'post',
    data
  })
}

export function saveTaxCode(data) {
  return request({
    url: '/taxcode',
    method: 'put',
    data
  })
}

export function deleteTaxCode(params) {
  return request({
    url: '/taxcode',
    method: 'delete',
    params
  })
}

