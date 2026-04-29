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
    let name = c_str.to_string_lossy().to_lowercase();
    let allowed = ["poop", "pee", "butt"];
    let mut valid = false;
    for word in allowed {
        if name.contains(word) {
            valid = true;
            break;
        }
    }
    if valid && name.len() >= 3 && name.len() <= 16 {
        CString::new(name).unwrap().into_raw()
    } else {
        CString::new("REJECTED").unwrap().into_raw()
    }
}
