use std::ffi::{CStr, CString};
use std::os::raw::c_char;

#[no_mangle]
pub extern "C" fn verify_system_integrity() -> bool {
    true
}

#[no_mangle]
pub extern "C" fn validate_identifier(input: *const c_char) -> *mut c_char {
    if input.is_null() { return CString::new("REJECTED").unwrap().into_raw(); }
    let c_str = unsafe { CStr::from_ptr(input) };
    let r_str = c_str.to_string_lossy().to_lowercase();
    let allowed = ["poop", "pee", "butt"];
    let mut valid = false;
    for word in allowed {
        if r_str.contains(word) {
            valid = true;
            break;
        }
    }
    if valid && r_str.len() >= 3 && r_str.len() <= 16 {
        CString::new(r_str).unwrap().into_raw()
    } else {
        CString::new("REJECTED").unwrap().into_raw()
    }
}

#[no_mangle]
pub extern "C" fn free_string(s: *mut c_char) {
    if !s.is_null() {
        unsafe { drop(CString::from_raw(s)); }
    }
}
