import { Link } from "react-router-dom";
import { routes } from "../../routes";
import { useRef, useState } from "react";

const initState = {
	email: "",
	password: "",
};

export default function SignInPage() {
	const emailRef = useRef();
	const passwordRef = useRef();
	const [showPw, setShowPw] = useState(false);
	const [errors, setErrors] = useState(initState);
	const validatePassword = (errors, password) => {
		if (password === "") {
			errors.password = "Vui lòng nhập mật khẩu!";
		} else if (password.length < 6) {
			errors.password = "Mật khẩu tối thiểu 6 ký tự!";
		}
	};
	const validateEmail = (errors, username) => {
		if (username === "") {
			errors.email = "Vui lòng nhập email!";
		}
	};
	const handleSubmit = (e) => {
		e.preventDefault();
		const username = emailRef.current.value;
		const password = passwordRef.current.value;
		const errors = {};
		validateEmail(errors, username);
		validatePassword(errors, password);
		if (Object.keys(errors).length) {
			setErrors(errors);
		} else {
			setErrors({
				...initState,
			});
		}
	};
	const handleChange = (name) => {
		setErrors({
			...errors,
			[name]: "",
		});
	};
	return (
		<div className="container-xxl bg-white d-flex p-0">
			<div className="container-fluid">
				<div className="row h-100 min-vh-100 align-items-center justify-content-center">
					<div className="col-12 col-sm-8 col-md-6 col-lg-5 col-xl-4">
						{/* should use form tag */}
						<form onSubmit={handleSubmit} className="bg-light bg-gradient rounded p-3 p-sm-4 my-4 mx-3">
							<div className="d-flex align-items-center justify-content-between mb-3">
								<Link to={routes.home}>
									<h4 className="text-primary text-uppercase">Trang chủ</h4>
								</Link>
								<h4>Đăng nhập</h4>
							</div>
							<div className="mb-3">
								<label htmlFor="Username" className="form-label">
									Email
								</label>
								<input
									ref={emailRef}
									onChange={() => {
										handleChange("email");
									}}
									className={`form-control ${errors.email ? "is-invalid" : ""}`}
									id="Username"
									type="email"
								/>
								<div className="invalid-feedback">{errors.email}</div>
							</div>
							<div className="mb-4">
								<label htmlFor="Password" className="form-label">
									Mật khẩu
								</label>
								<input
									ref={passwordRef}
									onChange={() => {
										handleChange("password");
									}}
									type={`${showPw ? "text" : "password"}`}
									className={`form-control ${errors.password ? "is-invalid" : ""}`}
									id="Password"
								/>
								<div className="invalid-feedback">{errors.password}</div>
							</div>
							<div className="d-flex flex-column flex-md-row align-items-center justify-content-between mb-4">
								<div className="form-check">
									<input
										onChange={(e) => {
											if (e.target.checked) {
												setShowPw(true);
											} else {
												setShowPw(false);
											}
										}}
										type="checkbox"
										id="check-password"
										className="form-check-input"
									/>
									<label htmlFor="check-password" className="form-check-label">
										Hiện mật khẩu!
									</label>
								</div>
								<a href="#">Quên mật khẩu?</a>
							</div>
							<button type="submit" className="btn btn-primary w-100 py-3 mb-4">
								Đăng nhập
							</button>
							<p className="text-center mb-0">
								{"Chưa có tài khoản? "}
								<Link to={routes.signUp}>Đăng ký</Link>
							</p>
						</form>
					</div>
				</div>
			</div>
		</div>
	);
}
