import request from '@/utils/request'

export function getMatchInvoiceRepost(params) {
  return request({
    url: '/report/getMatchInvoiceRepost',
    method: 'get',
    params
  })
}
