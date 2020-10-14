// import Cookies from 'js-cookie'

// const TokenKey = 'Best_Practice_Web_token'

// export function getToken() {
//   return Cookies.get(TokenKey)
// }

// export function setToken(token) {
//   return Cookies.set(TokenKey, token)
// }

// export function removeToken() {
//   return Cookies.remove(TokenKey)
// }

// localStorage 存储Token

export function getToken() {
  return localStorage.getItem('JWTToken')
}

export function setToken(token) {
  return localStorage.setItem('JWTToken', token)
}

export function removeToken() {
  return localStorage.removeItem('JWTToken')
}
