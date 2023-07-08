import { useDispatch, useSelector } from "react-redux";
import { selectAccessToken, selectRefreshToken, selectUser } from "../../redux/selectors";
import { useRef, useState } from "react";
import { validateEmail, validatePassword, validatePhone } from "../../utils/helpers";
import { axiosJWT, url } from "../../utils/httpRequest";
import { getUser } from "../../services/userServices";
import { toast } from "react-toastify";

const initErrorState = {
	lastName: "",
	firstName: "",
	age: "",
	email: "",
	phone: "",
};

export default function InfoTab() {
	const [newAvatar, setNewAvatar] = useState(null);
	const [errors, setErrors] = useState(initErrorState);

	const currentUser = useSelector(selectUser);
	const accessToken = useSelector(selectAccessToken);
	const refreshToken = useSelector(selectRefreshToken);
	const dispatch = useDispatch();

	const inputFileRef = useRef();
	const firstNameRef = useRef();
	const lastNameRef = useRef();
	const ageRef = useRef();
	const emailRef = useRef();
	const phoneRef = useRef();

	const handleChooseFile = () => {
		inputFileRef.current.value = "";
		inputFileRef.current.click();
	};
	const handleFileChange = (e) => {
		const file = e.target.files[0];
		file.preview = URL.createObjectURL(file);
		setNewAvatar(file);
	};
	const handleSubmit = (e) => {
		e.preventDefault();
		const lastName = lastNameRef.current.value;
		const firstName = firstNameRef.current.value;
		const age = ageRef.current.value;
		const email = emailRef.current.value;
		const phone = phoneRef.current.value;
		const errors = {};
		if (lastName === "") {
			errors.lastName = "Vui lòng nhập họ!";
		}
		if (firstName === "") {
			errors.firstName = "Vui lòng nhập tên!";
		}
		if (age === "") {
			errors.age = "Vui lòng nhập tuổi!";
		}
		validateEmail(errors, email);
		validatePhone(errors, phone);
		if (Object.keys(errors).length) {
			setErrors(errors);
		} else {
			setErrors({
				...initErrorState,
			});
			updateInforRequest();
		}
	};

	const updateInforRequest = async () => {
		const formData = new FormData();
		formData.append("LastName", lastNameRef.current.value);
		formData.append("FirstName", firstNameRef.current.value);
		formData.append("Age", ageRef.current.value);
		formData.append("Email", emailRef.current.value);
		formData.append("PhoneNumber", phoneRef.current.value);
		formData.append("Avatar", newAvatar);
		const axiosInstance = axiosJWT(accessToken, refreshToken, dispatch);
		const toastId = toast.loading("Đang cập nhật!");
		try {
			const res = await axiosInstance.put(url.editProfile, formData, {
				headers: {
					Authorization: `Bearer ${accessToken}`,
					"Content-Type": "multipart/form-data",
				},
			});
			if (res.data.success) {
				toast.update(toastId, {
					render: res.data.message,
					type: "success",
					closeButton: true,
					autoClose: 1000,
					isLoading: false,
				});
				// getUser(accessToken, refressToken, dispatch);
				getUser(accessToken, refreshToken, dispatch);
				// const resUser = await getUser(accessToken, refreshToken, dispatch);
				// if (resUser.isSuccess) {
				// 	dispatch(
				// 		updateUser({
				// 			_id: resUser.data._id,
				// 			name: resUser.data.name,
				// 			avatar: resUser.data.avatar,
				// 			phone: resUser.data.phone,
				// 			email: resUser.data.email,
				// 			username: resUser.data.username,
				// 			savedJobs: resUser.data.jobFavourite,
				// 		}),
				// 	);
				// }
			}
		} catch (error) {
			console.log(error);
			toast.update(toastId, {
				render: "Cập nhật thất bại!",
				type: "error",
				closeButton: true,
				autoClose: 1000,
				isLoading: false,
			});
			// if (!error.response.data.isSuccess) {
			// 	toast.error(error.response.data.message);
			// }
		}
	};

	return (
		<div className="bg-white rounded p-5 mt-3">
			<div className="d-flex flex-column px-5">
				<form onSubmit={handleSubmit}>
					<div className="d-flex flex-column">
						<div className="mb-3 row">
							<aside className="col-sm-3 d-flex">
								<div className="border border-2 rounded-circle p-1 overflow-hidden">
									{newAvatar ? (
										<img
											className="rounded-circle"
											src={newAvatar.preview}
											// src={"/img/user-avatar.png"}
											alt=""
											style={{ width: 60, height: 60, objectFit: "cover" }}
											onError={(e) => {
												e.target.src = "/img/user-avatar.png";
											}}
										/>
									) : (
										<img
											className="rounded-circle"
											src={currentUser.avatar ? currentUser.avatar : "/img/user-avatar.png"}
											// src={"/img/user-avatar.png"}
											alt=""
											style={{ width: 60, height: 60, objectFit: "cover" }}
											onError={(e) => {
												e.target.src = "/img/user-avatar.png";
											}}
										/>
									)}
								</div>
							</aside>
							<div className="col-sm-9 d-flex flex-column justify-content-center">
								<span className="fw-bold">{currentUser.lastName + " " + currentUser.firstName}</span>
								<div>
									<span onClick={handleChooseFile} className="cursor-pointer text-primary">
										Thay đổi ảnh đại diện
									</span>
								</div>
								<input
									ref={inputFileRef}
									onChange={handleFileChange}
									className="d-none form-control"
									type="file"
									placeholder="input"
									accept="image/*"
								/>
							</div>
						</div>
						<div className="mb-3 row">
							<label htmlFor="" className="col-sm-3 col-form-label">
								Họ
							</label>
							<div className="col-sm-9">
								<input
									ref={lastNameRef}
									type="text"
									defaultValue={currentUser.lastName}
									className={`form-control ${errors.lastName ? "is-invalid" : ""}`}
								/>
								<div className="invalid-feedback">{errors.lastName}</div>
							</div>
						</div>
						<div className="mb-3 row">
							<label htmlFor="" className="col-sm-3 col-form-label">
								Tên
							</label>
							<div className="col-sm-9">
								<input
									ref={firstNameRef}
									type="text"
									defaultValue={currentUser.firstName}
									className={`form-control ${errors.firstName ? "is-invalid" : ""}`}
								/>
								<div className="invalid-feedback">{errors.firstName}</div>
							</div>
						</div>
						<div className="mb-3 row">
							<label htmlFor="" className="col-sm-3 col-form-label">
								Tuổi
							</label>
							<div className="col-sm-9">
								<input
									ref={ageRef}
									type="number"
									defaultValue={currentUser.age}
									min={0}
									className={`form-control ${errors.age ? "is-invalid" : ""}`}
								/>
								<div className="invalid-feedback">{errors.age}</div>
							</div>
						</div>
						<div className="mb-3 row">
							<label htmlFor="" className="col-sm-3 col-form-label">
								Email
							</label>
							<div className="col-sm-9">
								<input
									ref={emailRef}
									type="email"
									defaultValue={currentUser.email}
									className={`form-control ${errors.email ? "is-invalid" : ""}`}
								/>
								<div className="invalid-feedback">{errors.email}</div>
							</div>
						</div>
						<div className="mb-3 row">
							<label htmlFor="" className="col-sm-3 col-form-label">
								Số điện thoại
							</label>
							<div className="col-sm-9">
								<input
									ref={phoneRef}
									type="text"
									defaultValue={currentUser.phoneNumber}
									className={`form-control ${errors.phone ? "is-invalid" : ""}`}
								/>
								<div className="invalid-feedback">{errors.phone}</div>
							</div>
						</div>
						<div className="mb-3 row">
							<label className="col-sm-3 col-form-label"></label>
							<div className="col-sm-9">
								<button type="submit" className="btn btn-outline-primary w-100">
									Cập nhật
								</button>
							</div>
						</div>
					</div>
				</form>
			</div>
		</div>
	);
}
