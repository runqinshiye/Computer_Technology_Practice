#[no_mangle]//不写这个会把函数名编译成其他乱七八糟的
pub extern fn output_int(n:i32)->i32{
    println!("Rust Dll get i32: {}",n);
    n+100
}
