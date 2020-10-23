import request from '@/utils/request'

export function getVendorList(params) {
  return request({
    url: '/vendor',
    method: 'get',
    params
  })
}

export function addVendor(data) {
  return request({
    url: '/vendor',
    method: 'post',
    data
  })
}

export function saveVendor(data) {
  return request({
    url: '/vendor',
    method: 'put',
    data
  })
}

