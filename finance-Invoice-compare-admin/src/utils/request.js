import axios from 'axios'
import { MessageBox, Message } from 'element-ui'
import store from '@/store'
import { getToken } from '@/utils/auth'
import router from '@/router'

// create an axios instance
const service = axios.create({
  baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
  // withCredentials: true, // send cookies when cross-domain requests
  timeout: 50000 // request timeout
})

// request interceptor
service.interceptors.request.use(
  config => {
    // do something before request is sent
    if (store.getters.token) {
      config.headers = {
        'Authorization': `Bearer ${getToken()}`
      }
    }
    return config
  },
  error => {
    // do something with request error
    console.log(error) // for debug
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
  */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    console.log(response)
    const res = response.data
    if (res) {
      switch (res.status) {
        case 200:
          return res
        default:
          Message({
            message: res.message || 'Error',
            type: 'error',
            duration: 5 * 1000
          })
          return Promise.reject(res.message)
      }
    }
  },
  error => {
    if (error.response) {
      switch (error.response.status) {
        case 401:
          MessageBox.confirm('登录时间超时，需要重新登录才能继续操作！', '提示', {
            confirmButtonText: '重新登录',
            cancelButtonText: '取消',
            type: 'warning'
          }).then(() => {
            store.dispatch('user/resetToken').then(() => {
              router.push({
                path: '/login'
              })
            })
          })
          break
        case 422:
          Message({
            message: error.response.message || 'Error',
            type: 'error',
            duration: 5 * 1000
          })
          break
        default:
          Message({
            message: error.response.message || 'Error',
            type: 'error',
            duration: 5 * 1000
          })
          break
      }
    } else {
      Message({
        message: error.message || 'Error',
        type: 'error',
        duration: 5 * 1000
      })
    }
    return Promise.reject(error)
  }
)

export default service
