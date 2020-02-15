import * as React from 'react';
import { ModalProps } from '~/common/components/Modal/Modal';
import { Button } from '~/emby/components/Button';

export const Modal: React.FC<ModalProps> = props => {
    return (
        <>
            <div className="dialogBackdrop dialogBackdropOpened" />
            <div className="dialogContainer">
                <div
                    className="focuscontainer dialog dialog-fixedSize dialog-medium-tall formDialog opened"
                    data-lockscroll="true"
                    data-history="true"
                    data-autofocus="true"
                    data-removeonclose="true"
                >
                    <div className="formDialogHeader">
                        <button is="paper-icon-button-light" onClick={_ => props.onClose()}>
                            <i className="md-icon"></i>
                        </button>
                        <h3 className="formDialogHeaderTitle">{props.title}</h3>
                    </div>
                    <div className="formDialogContent scrollY">
                        <div className="dialogContentInner">{props.children}</div>
                        <div className="formDialogFooter">
                            <Button
                                type="submit"
                                onClick={_ => props.onConfirm()}
                                isBlock={true}
                                class="formDialogFooterItem"
                            >
                                {props.confirmLabel}
                            </Button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
};
