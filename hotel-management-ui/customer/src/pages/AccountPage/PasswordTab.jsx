import { useState } from "react";
import { useRef } from "react";

import { validatePassword } from "../../utils/helpers";
import { toast } from "react-toastify";
import { axiosJWT, url } from "../../utils/httpRequest";
import { useDispatch, useSelector } from "react-redux";
import { selectAccessToken, selectRefreshToken } from "../../redux/selectors";

const initErrorState = {
	oldPw: "",
	newPw: "",
	confirmPw: "",
};

export default function PasswordTab() {
	const accessToken = useSelector(selectAccessToken);
	const refreshToken = useSelector(selectRefreshToken);
	const dispatch = useDispatch();

	const oldPwRef = useRef();
	const newPwRef = useRef();
	const confirmPwRef = useRef();

	const [showOldPw, setShowOldPw] = useState(false);
	const [showNewPw, setShowNewPw] = useState(false);
	const [showConfirmPw, setShowConfirmPw] = useState(false);
	const [errors, setErrors] = useState(initErrorState);

	const handleSubmit = (e) => {
		e.preventDefault();
		const oldPw = oldPwRef.current.value;
		const newPw = newPwRef.current.value;
		const confirmPw = confirmPwRef.current.value;

		const errors = {};

		validatePassword(errors, oldPw, "oldPw");
		validatePassword(errors, newPw, "newPw");
		validatePassword(errors, confirmPw, "confirmPw");
		if (confirmPw !== newPw) {
			errors.confirmPw = "Mật khẩu xác nhận không khớp!";
		}

		if (Object.keys(errors).length) {
			setErrors(errors);
			console.log(errors);
		} else {
			setErrors({
				...initErrorState,
			});
			changePwReq(oldPw, newPw);
		}
	};

	const changePwReq = async (oldPw, newPw) => {
		const toastId = toast.loading("Đang xử lý...");
		const axiosInstance = axiosJWT(accessToken, refreshToken, dispatch);
		try {
			const res = await axiosInstance.patch(
				url.changePw,
				{
					oldPassword: oldPw,
					newPassword: newPw,
				},
				{
					headers: {
						Authorization: "Bearer " + accessToken,
					},
				},
			);
			console.log(res);
			if (res.data.success) {
				oldPwRef.current.value = "";
				newPwRef.current.value = "";
				confirmPwRef.current.value = "";
				toast.update(toastId, {
					render: "Đổi mật khẩu thành công!",
					type: "success",
					closeButton: true,
					autoClose: 1000,
					isLoading: false,
				});
			}
		} catch (error) {
			console.log(error);
			toast.update(toastId, {
				render: "Mật khẩu cũ không đúng!",
				type: "error",
				closeButton: true,
				autoClose: 1000,
				isLoading: false,
			});
		}
	};

	return (
		<div className="bg-white rounded p-4 mt-3">
			<div className="d-flex flex-column px-5">
				<form onSubmit={handleSubmit}>
					<div className="mb-3 row">
						<label htmlFor="" className="col-sm-4 col-form-label">
							Mật khẩu cũ
						</label>
						<div className="position-relative col-sm-7">
							<input
								ref={oldPwRef}
								onChange={() => {
									setErrors({
										...errors,
										oldPw: "",
									});
								}}
								type={`${showOldPw ? "text" : "password"}`}
								className={`form-control ${errors.oldPw ? "is-invalid" : ""}`}
							/>
							<span
								onClick={() => {
									setShowOldPw(!showOldPw);
								}}
								className="input-right-icon row-label">
								{showOldPw ? <i className="fa-solid fa-eye-slash"></i> : <i className="fa-solid fa-eye"></i>}
							</span>
							<div className="invalid-feedback">{errors.oldPw}</div>
						</div>
					</div>
					<div className="mb-3 row">
						<label htmlFor="" className="col-sm-4 col-form-label">
							Mật khẩu mới
						</label>
						<div className="position-relative col-sm-7">
							<input
								ref={newPwRef}
								onChange={() => {
									setErrors({
										...errors,
										newPw: "",
									});
								}}
								type={`${showNewPw ? "text" : "password"}`}
								className={`form-control ${errors.newPw ? "is-invalid" : ""}`}
							/>
							<span
								onClick={() => {
									setShowNewPw(!showNewPw);
								}}
								className="input-right-icon row-label">
								{showNewPw ? <i className="fa-solid fa-eye-slash"></i> : <i className="fa-solid fa-eye"></i>}
							</span>
							<div className="invalid-feedback">{errors.newPw}</div>
						</div>
					</div>
					<div className="mb-3 row">
						<label htmlFor="" className="col-sm-4 col-form-label">
							Xác nhận mật khẩu
						</label>
						<div className="position-relative col-sm-7">
							<input
								ref={confirmPwRef}
								onChange={() => {
									setErrors({
										...errors,
										confirmPw: "",
									});
								}}
								type={`${showConfirmPw ? "text" : "password"}`}
								className={`form-control ${errors.confirmPw ? "is-invalid" : ""}`}
							/>
							<span
								onClick={() => {
									setShowConfirmPw(!showConfirmPw);
								}}
								className="input-right-icon row-label">
								{showConfirmPw ? <i className="fa-solid fa-eye-slash"></i> : <i className="fa-solid fa-eye"></i>}
							</span>
							<div className="invalid-feedback">{errors.confirmPw}</div>
						</div>
					</div>
					<div className="row">
						<label className="col-sm-4 col-form-label"></label>
						<div className="col-sm-7">
							<button type="submit" className="btn btn-outline-primary w-100">
								Đổi mật khẩu
							</button>
						</div>
					</div>
				</form>
			</div>
		</div>
	);
}
