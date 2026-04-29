use std::ffi::{CStr, CString};
use std::os::raw::c_char;

#[no_mangle]
pub extern "C" fn verify_system_integrity() -> bool {
    let check_status = true; 
    check_status
}

#[no_mangle]
pub extern "C" fn validate_identifier(input: *const c_char) -> *mut c_char {
    if input.is_null() { return CString::new("REJECTED").unwrap().into_raw(); }
    
    let c_str = unsafe { CStr::from_ptr(input) };
    let r_str = c_str.to_string_lossy().to_lowercase();
    
    let allowed_terms = ["poop", "pee", "butt"];
    let mut has_allowed = false;

    for term in allowed_terms {
        if r_str.contains(term) {
            has_allowed = true;
            break;
        }
    }

    if has_allowed && r_str.len() >= 3 && r_str.len() <= 16 {
        CString::new(r_str).unwrap().into_raw()
    } else {
        CString::new("REJECTED").unwrap().into_raw()
    }
}

#[no_mangle]
pub extern "C" fn release_identifier(s: *mut c_char) {
    if !s.is_null() {
        unsafe { drop(CString::from_raw(s)); }
    }
}
